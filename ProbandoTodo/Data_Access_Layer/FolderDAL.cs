﻿using Domain_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Data_Access_Layer
{
    public class FolderDAL
    {        
        public IEnumerable<Folder> GetAllFoldersDAL()
        {
            try
            {
                using(var context = new WinNotesDBEntities())
                {
                    IEnumerable<Folder> folders = context.Folder.ToList();
                    return folders;
                }
            }
            catch
            {
                throw new NullReferenceException("No se pudo recuperar el listado de carpetas");
            }
        }

        public void CreateFolderDAL(int id, string name, string details)
        {
            try
            {
                using (var context = new WinNotesDBEntities())
                {
                    if(!context.Folder.Any(f => f.Name == name))
                    {
                        Folder folder = new Folder();
                        folder.Name = name;
                        folder.Details = details;
                        folder.LastModified = DateTime.Now;
                        folder.Person_ID = id;
                        context.Folder.Add(folder);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new ArgumentException("Ya existe una carpeta con ese nombre");
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public Folder GetFolderDataDAL(int folderID)
        {
            try
            {
                using (var context = new WinNotesDBEntities())
                {
                    Folder folder = context.Folder.Where(f => f.FolderID == folderID).First();
                    return folder;
                }                
            }
            catch(Exception ex)
            {                
                throw ex;
            }
        }

        public List<Note> GetNotesInFolderDAL(int userID, int folderID)
        {
            try
            {
                using (var context = new WinNotesDBEntities())
                {
                    List<Note> notes = context.Note.Where(n => n.Folder_ID == folderID && n.Person_ID == userID).ToList();                    
                    return notes;
                }                
            }            
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<SelectListItem> GetFoldersOfUserDAL(int userID)
        {
            try
            {
                using (var context = new WinNotesDBEntities())
                {                    
                    List<Folder> folders = context.Folder.Where(f => f.Person_ID == userID).ToList();
                    List<SelectListItem> folderComboBox = new List<SelectListItem>();
                    foreach (var folder in folders)                    
                        folderComboBox.Add(new SelectListItem { Value = folder.Name, Text = folder.Name });
                                        
                    return folderComboBox;
                }                    
            }
            catch
            {
                List<SelectListItem> folderComboBox = new List<SelectListItem>();
                folderComboBox.Add(new SelectListItem { Value = "error", Text = "se ha producido un error" });
                return folderComboBox;
            }
        }

        public bool ChangeFolderDAL(int noteID, int userID, string folderSelected)
        {
            try
            {
                using (var context = new WinNotesDBEntities())
                {                    
                    int folderID = context.Folder.Where(f => f.Name == folderSelected && f.Person_ID == userID).First().FolderID;
                    Note note = context.Note.Where(n => n.NoteID == noteID).First();
                    if (note.Completed != true)
                    {
                        note.Folder_ID = folderID;
                        context.SaveChanges();
                    }
                    return true;
                }                    
            }
            catch
            {
                return false;
            }
        }
    }
}
