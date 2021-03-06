﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Domain_Layer
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class WinNotesEntities : DbContext
    {
        public WinNotesEntities()
            : base("name=WinNotesEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Folder> Folder { get; set; }
        public virtual DbSet<Note> Note { get; set; }
        public virtual DbSet<Person> Person { get; set; }
    
        public virtual int sp_changeAvatar(Nullable<int> userID, byte[] avatarImage, string mimeType)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("userID", userID) :
                new ObjectParameter("userID", typeof(int));
    
            var avatarImageParameter = avatarImage != null ?
                new ObjectParameter("avatarImage", avatarImage) :
                new ObjectParameter("avatarImage", typeof(byte[]));
    
            var mimeTypeParameter = mimeType != null ?
                new ObjectParameter("mimeType", mimeType) :
                new ObjectParameter("mimeType", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_changeAvatar", userIDParameter, avatarImageParameter, mimeTypeParameter);
        }
    
        public virtual int sp_changeNoteLocation(Nullable<int> userID, Nullable<int> noteID, string selectedFolder)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("userID", userID) :
                new ObjectParameter("userID", typeof(int));
    
            var noteIDParameter = noteID.HasValue ?
                new ObjectParameter("noteID", noteID) :
                new ObjectParameter("noteID", typeof(int));
    
            var selectedFolderParameter = selectedFolder != null ?
                new ObjectParameter("selectedFolder", selectedFolder) :
                new ObjectParameter("selectedFolder", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_changeNoteLocation", userIDParameter, noteIDParameter, selectedFolderParameter);
        }
    
        public virtual int sp_changePassword(Nullable<int> userID, string currentPassword, string newPassword)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("userID", userID) :
                new ObjectParameter("userID", typeof(int));
    
            var currentPasswordParameter = currentPassword != null ?
                new ObjectParameter("currentPassword", currentPassword) :
                new ObjectParameter("currentPassword", typeof(string));
    
            var newPasswordParameter = newPassword != null ?
                new ObjectParameter("newPassword", newPassword) :
                new ObjectParameter("newPassword", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_changePassword", userIDParameter, currentPasswordParameter, newPasswordParameter);
        }
    
        public virtual int sp_changePersonalPhrase(Nullable<int> userID, string phrase, string phraseColor)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("userID", userID) :
                new ObjectParameter("userID", typeof(int));
    
            var phraseParameter = phrase != null ?
                new ObjectParameter("phrase", phrase) :
                new ObjectParameter("phrase", typeof(string));
    
            var phraseColorParameter = phraseColor != null ?
                new ObjectParameter("phraseColor", phraseColor) :
                new ObjectParameter("phraseColor", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_changePersonalPhrase", userIDParameter, phraseParameter, phraseColorParameter);
        }
    
        public virtual int sp_createNewFolder(Nullable<int> userID, string name, string details)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("userID", userID) :
                new ObjectParameter("userID", typeof(int));
    
            var nameParameter = name != null ?
                new ObjectParameter("name", name) :
                new ObjectParameter("name", typeof(string));
    
            var detailsParameter = details != null ?
                new ObjectParameter("details", details) :
                new ObjectParameter("details", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_createNewFolder", userIDParameter, nameParameter, detailsParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> sp_createNewUser(string userName, string email, string password)
        {
            var userNameParameter = userName != null ?
                new ObjectParameter("userName", userName) :
                new ObjectParameter("userName", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("email", email) :
                new ObjectParameter("email", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("sp_createNewUser", userNameParameter, emailParameter, passwordParameter);
        }
    
        public virtual int sp_editFolder(Nullable<int> userID, Nullable<int> folderID, string name, string details)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("userID", userID) :
                new ObjectParameter("userID", typeof(int));
    
            var folderIDParameter = folderID.HasValue ?
                new ObjectParameter("folderID", folderID) :
                new ObjectParameter("folderID", typeof(int));
    
            var nameParameter = name != null ?
                new ObjectParameter("name", name) :
                new ObjectParameter("name", typeof(string));
    
            var detailsParameter = details != null ?
                new ObjectParameter("details", details) :
                new ObjectParameter("details", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_editFolder", userIDParameter, folderIDParameter, nameParameter, detailsParameter);
        }
    
        public virtual ObjectResult<sp_getUserFolders_Result> sp_getUserFolders(Nullable<int> userID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("userID", userID) :
                new ObjectParameter("userID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_getUserFolders_Result>("sp_getUserFolders", userIDParameter);
        }
    
        public virtual ObjectResult<sp_getUserInformation_Result> sp_getUserInformation(Nullable<int> userID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("userID", userID) :
                new ObjectParameter("userID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_getUserInformation_Result>("sp_getUserInformation", userIDParameter);
        }
    
        public virtual ObjectResult<sp_login_Result> sp_login(string email, string password)
        {
            var emailParameter = email != null ?
                new ObjectParameter("email", email) :
                new ObjectParameter("email", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("password", password) :
                new ObjectParameter("password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_login_Result>("sp_login", emailParameter, passwordParameter);
        }
    
        public virtual int sp_refreshLoginDate(Nullable<int> id_user)
        {
            var id_userParameter = id_user.HasValue ?
                new ObjectParameter("id_user", id_user) :
                new ObjectParameter("id_user", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_refreshLoginDate", id_userParameter);
        }
    
        public virtual int sp_removeFolder(Nullable<int> userID, Nullable<int> folderID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("userID", userID) :
                new ObjectParameter("userID", typeof(int));
    
            var folderIDParameter = folderID.HasValue ?
                new ObjectParameter("folderID", folderID) :
                new ObjectParameter("folderID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_removeFolder", userIDParameter, folderIDParameter);
        }
    
        public virtual int sp_saveEncryptedUserID(Nullable<int> id, string encryptedID)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            var encryptedIDParameter = encryptedID != null ?
                new ObjectParameter("encryptedID", encryptedID) :
                new ObjectParameter("encryptedID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_saveEncryptedUserID", idParameter, encryptedIDParameter);
        }
    
        public virtual int sp_verifyUserName(string userName, ObjectParameter result)
        {
            var userNameParameter = userName != null ?
                new ObjectParameter("userName", userName) :
                new ObjectParameter("userName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_verifyUserName", userNameParameter, result);
        }
    }
}
