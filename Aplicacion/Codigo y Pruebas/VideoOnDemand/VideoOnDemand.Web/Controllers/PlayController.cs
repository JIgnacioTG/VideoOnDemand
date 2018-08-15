using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoOnDemand.Entities;
using VideoOnDemand.Repositories;
using VideoOnDemand.Web.Helpers;
using VideoOnDemand.Web.Models;

namespace VideoOnDemand.Web.Controllers
{
    public class PlayController : BaseController
    {
        // GET: Play
        public ActionResult Index(int MediaId, string UserId)
        {
            UsuarioRepository usuarioRepository = new UsuarioRepository(context);
            MediaOnPlayRepository mediaonplayRepository = new MediaOnPlayRepository(context);
            Usuario usuario = usuarioRepository.Query(u => u.IdentityId == UserId).FirstOrDefault();
            MediaOnPlay mediaOnPlay = mediaonplayRepository.Query(m => m.MediaId == MediaId && m.UsuarioId == usuario.Id).FirstOrDefault();
            MediaOnPlayViewModel model;

            // Si el usuario no existe.
            if (usuario == null)
            {
                ViewBag.Error = 1;
                return View();
            }

            // Si el usuario nunca ha visto la pelicula o serie.
            if (mediaOnPlay == null)
            {
                MediaRepository mediaRepository = new MediaRepository(context);
                var media = mediaRepository.Query(m => m.id == MediaId && m.estado != EEstatusMedia.ELIMINADO && m.estado != EEstatusMedia.INVISIBLE).FirstOrDefault();
                // Si la pelicula o serie que desea ver no esta disponible.
                if (media == null)
                {
                    ViewBag.Error = 2;
                    return View();
                }
                mediaOnPlay = new MediaOnPlay { Milisegundo = 0,
                                                Media = media,
                                                MediaId = media.id,
                                                Usuario = usuario,
                                                UsuarioId = usuario.Id };
                mediaonplayRepository.Insert(mediaOnPlay);

                context.SaveChanges();

                model = MapHelper.Map<MediaOnPlayViewModel>(mediaOnPlay);
            }

            else
            {
                // Se recarga la entidad.
                context.Entry(mediaOnPlay).Reference(m => m.Usuario).Load();
                context.Entry(mediaOnPlay).Reference(m => m.Media).Load();

                model = MapHelper.Map<MediaOnPlayViewModel>(mediaOnPlay);
            }

            ViewBag.Title = "Reproduciendo: " + mediaOnPlay.Media.nombre;
            return View(model);
        }

        // POST: Play/Pause
        [HttpPost]
        public ActionResult Update(MediaOnPlayViewModel model)
        {
            MediaOnPlayRepository mediaonplayRepository = new MediaOnPlayRepository(context);
            MediaOnPlay mediaOnPlay = mediaonplayRepository.Query(m => m.Id == model.Id).First();

            mediaOnPlay.Milisegundo = model.Milisegundo;
            mediaonplayRepository.Update(mediaOnPlay);

            context.SaveChanges();

            return Json(new
            {
                Success = true
            }, JsonRequestBehavior.AllowGet);
        }

        // POST: Play/Stop
        [HttpPost]
        public ActionResult Stop(int MediaId, int UserId)
        {
            return View();
        }

    }
}
