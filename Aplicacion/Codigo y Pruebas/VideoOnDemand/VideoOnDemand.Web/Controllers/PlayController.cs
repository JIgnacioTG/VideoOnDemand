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

        // GET: Play/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Play/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Play/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Play/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Play/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Play/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Play/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
