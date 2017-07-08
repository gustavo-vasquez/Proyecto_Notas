﻿using Business_Logic_Layer;
using Domain_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static ProbandoTodo.Filters.CustomFilters;
using static ProbandoTodo.Models.FolderModels;
using static ProbandoTodo.Models.NoteModels;

namespace ProbandoTodo.Controllers
{
    [WithAccount]
    public class NoteController : Controller
    {
        static NoteBLL noteBLL = new NoteBLL();        
        
        public ActionResult Create()
        {            
            int userID = UserLoginData.GetSessionID(Session["UserLoggedIn"]);
            CreateNoteModelView model = new CreateNoteModelView();
            this.PrepareModelToCreateNote(userID, ref model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateNoteModelView model)
        {
            try
            {                
                int userID = UserLoginData.GetSessionID(Session["UserLoggedIn"]);

                if (!ModelState.IsValid)
                {
                    this.PrepareModelToCreateNote(userID, ref model);
                    return View(model);
                }

                int folderAuxID = 0;
                noteBLL.CreateNoteBLL(
                                    userID,
                                    model.Title,
                                    model.Details,
                                    model.ExpirationDate,
                                    model.Starred,
                                    model.FolderSelected,
                                    model.HourSelected,
                                    model.MinuteSelected,
                                    model.TimeTableSelected,
                                    ref folderAuxID
                                   );

                return RedirectToAction("NotesList", "Folder", new { folderID = folderAuxID });
            }
            catch(Exception ex)
            {
                TempData["error"] = ex.Message;
                this.PrepareModelToCreateNote(UserLoginData.GetSessionID(Session["UserLoggedIn"]), ref model);
                return View(model);
            }
        }

        private void PrepareModelToCreateNote(int userID, ref CreateNoteModelView model)
        {
            string[] userInfoBox = noteBLL.GetUserBoxInfoBLL(userID);
            model.FoldersBox = noteBLL.GetFoldersToSelectBLL(userID);
            model.HourBox = noteBLL.GenerateHourCombo();
            model.MinuteBox = noteBLL.GenerateMinuteCombo();
            model.TimeTableBox = noteBLL.GenerateTimeTableCombo();
            model.UserName = userInfoBox[0];
            model.PersonalPhrase = userInfoBox[1];
            model.PhraseColor = userInfoBox[2];
            model.AvatarSrc = userInfoBox[3];
            model.FoldersCount = Convert.ToInt32(userInfoBox[4]);
            model.NotesCount = Convert.ToInt32(userInfoBox[5]);
        }
        
        public ActionResult List()
        {
            try
            {                
                int userID = UserLoginData.GetSessionID(Session["UserLoggedIn"]);
                return View(new ClassifiedQueryableNotes(noteBLL.GetDataForNoteList(userID)));
            }
            catch(Exception ex)
            {
                TempData["error"] = ex.Message;
                return Redirect(Request.UrlReferrer.AbsolutePath);
            }
        }

        [HttpPost]
        public ActionResult ForceCompleteTask(int folderID, int noteID, bool inFolder)
        {
            try
            {                
                noteBLL.ForceCompleteTaskBLL(noteID);
                int userID = UserLoginData.GetSessionID(Session["UserLoggedIn"]);

                if (inFolder)
                    return PartialView("~/Views/Folder/_NotesInFolder.cshtml", new ClassifiedNotes(new FolderBLL().GetNotesInFolderBLL(userID, folderID)));

                return PartialView("_ListOfNotes", new ClassifiedQueryableNotes(noteBLL.GetDataForNoteList(userID)));
            }
            catch(Exception ex)
            {
                return RedirectToAction("InternalServerError", "Error", new { error = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult StarTask(int folderID, int noteID, bool inFolder)
        {
            try
            {
                noteBLL.StarTaskBLL(noteID);
                int userID = UserLoginData.GetSessionID(Session["UserLoggedIn"]);

                if (inFolder)
                    return PartialView("~/Views/Folder/_NotesInFolder.cshtml", new ClassifiedNotes(new FolderBLL().GetNotesInFolderBLL(userID, folderID)));

                return PartialView("_ListOfNotes", new ClassifiedQueryableNotes(noteBLL.GetDataForNoteList(userID)));
            }
            catch(Exception ex)
            {
                return RedirectToAction("InternalServerError", "Error", new { error = ex.Message });
            }
        }

        [HttpGet]
        public ActionResult ChangeDateTimeEventPartial(string currentDate, string previousDate, int idNote, int idFolder, bool inFolder)
        {            
            return PartialView("_ChangeDateTimeEvent", new ChangeDatetimeEventModel(currentDate, previousDate, idNote, idFolder, inFolder));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetDatetimeEvent(ChangeDatetimeEventModel model)
        {
            try
            {                
                int userID = UserLoginData.GetSessionID(Session["UserLoggedIn"]);
                noteBLL.ChangeDatetimeEventBLL(model.CurrentDate, model.HourSelected, model.MinuteSelected, model.TimeTableSelected, model.ID_Note, userID);
                if (model.InFolder)
                    return PartialView("~/Views/Folder/_NotesInFolder.cshtml", new ClassifiedNotes(new FolderBLL().GetNotesInFolderBLL(userID, model.ID_Folder)));
                else
                    return PartialView("_ListOfNotes", new ClassifiedQueryableNotes(noteBLL.GetDataForNoteList(userID)));
            }
            catch(Exception ex)
            {
                return RedirectToAction("InternalServerError", "Error", new { error = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult CheckExpiredEventsPartial(FireAlarmModel model)
        {
            try
            {
                if (model.EncryptedCookie == null)
                    model.EncryptedCookie = new UserBLL().GetEncryptedUserID(UserLoginData.GetSessionID(Session["UserLoggedIn"]));

                return PartialView("_FireAlarmDialog", noteBLL.CheckExpiredEventsBLL(model.EncryptedCookie));
            }
            catch (Exception ex)
            {
                return RedirectToAction("InternalServerError", "Error", new { error = ex.Message });
            }
        }
    }
}