using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notlarim102.BusinessLayer.Abstract;
using Notlarim102.Common.Helper;
using Notlarim102.DataAccessLayer.EntityFramework;
using Notlarim102.Entity;
using Notlarim102.Entity.Messages;
using Notlarim102.Entity.ValueObject;

namespace Notlarim102.BusinessLayer
{
    public class NotlarimUserManager:ManagerBase<NotlarimUser>
    {
        readonly BusinessLayerResult<NotlarimUser> res = new BusinessLayerResult<NotlarimUser>();
        public BusinessLayerResult<NotlarimUser> RegisterUser(RegisterViewModel data)
        {
            NotlarimUser user = Find(s => s.Username == data.Username || s.Email == data.Email);

            if (user != null)
            {
                if (user.Username == data.Username)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExist, "Kullanici adi daha once kaydedilmis.");
                }
                if (user.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExist, "Email daha once kullanilmis.");
                }
                //throw new Exception("Bu bilgiler daha once kullanilmis.");
            }
            else
            {
                DateTime now = DateTime.Now;
                int dbResult = base.Insert(new NotlarimUser()
                {
                    Username = data.Username,
                    Email = data.Email,
                    Password = data.Password,
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = false,
                    IsAdmin = false,
                    ProfileImageFilename = "user1.png"
                    //kapatilanlar repository de otamatik eklenecek sekilde duzenlenecektir.
                    //ModifiedOn = now,
                    //CreatedOn = now,
                    //ModifiedUsername = "system"
                });
                if (dbResult > 0)
                {
                    res.Result = Find(s => s.Email == data.Email && s.Username == data.Username);

                    string siteUri = ConfigHelper.Get<string>("SiteRootUri");
                    string activateUri = $"{siteUri}/Home/UserActivate/{res.Result.ActivateGuid}";
                    string body =
                        $"Merhaba {res.Result.Username};<br><br> Hesabinizi aktiflestirmek icin <a href='{activateUri}' target='_blank'> Tiklayin </a>.";
                    MailHelper.SendMail(body, res.Result.Email, "Notlarim102 hesap aktiflestirme");
                }
            }
            return res;
        }

        public BusinessLayerResult<NotlarimUser> LoginUser(LoginViewModel data)
        {
            //giris kontrolunu
            //hesap aktif mi kontrolu
            res.Result = Find(s => s.Username == data.Username && s.Password == data.Password);

            if (res.Result != null)
            {
                if (!res.Result.IsActive)
                {
                    res.AddError(ErrorMessageCode.UserIsNotActive, "Kullanici aktiflestirilmemis.");
                    res.AddError(ErrorMessageCode.CheckYourEmail, "Lutfen mailinizi kontrol edin");
                }
            }
            else
            {
                res.AddError(ErrorMessageCode.UsernameOrPassWrong, "Kullanici adi yada sifre yanlis");
            }
            return res;
        }

        public BusinessLayerResult<NotlarimUser> ActivateUser(Guid id)
        {
            res.Result = Find(x => x.ActivateGuid == id);
            if (res.Result != null)
            {
                if (res.Result.IsActive)
                {
                    res.AddError(ErrorMessageCode.UserAlreadyActive, "Bu hesap daha once aktif edilmistir.");
                    return res;
                }
                res.Result.IsActive = true;
                Update(res.Result);
            }
            else
            {
                res.AddError(ErrorMessageCode.ActivateIdDoesNotExist, "Ali Osman siteyi bi rahat birak.");
            }
            return res;
        }

        public BusinessLayerResult<NotlarimUser> GetUserById(int id)
        {
            res.Result = Find(s => s.Id == id);
            if (res.Result == null)
            {
                res.AddError(ErrorMessageCode.UserNotFound, "Kullanici bulunamadi.");
            }
            return res;
        }

        public BusinessLayerResult<NotlarimUser> UpdateProfile(NotlarimUser data)
        {
            NotlarimUser user =
                Find(x => x.Id != data.Id && (x.Username == data.Username || x.Email == data.Email));
            
            if (user != null && user.Id != data.Id)
            {
                if (user.Username == data.Username)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExist, "Bu kullanici adi daha once kullanilmis.");
                }
                if (user.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExist, "Bu e-posta daha once kullanilmis.");
                }
                return res;
            }
            res.Result = Find(s => s.Id == data.Id);
            res.Result.Email = data.Email;
            res.Result.Name = data.Name;
            res.Result.Surname = data.Surname;
            res.Result.Password = data.Password;
            res.Result.Username = data.Username;
            if (!string.IsNullOrEmpty(data.ProfileImageFilename))
            {
                res.Result.ProfileImageFilename = data.ProfileImageFilename;
            }
            if (base.Update(res.Result) == 0)
            {
                res.AddError(ErrorMessageCode.ProfileCouldNotUpdate, "Profil guncellenemedi");
            }
            return res;
        }

        public BusinessLayerResult<NotlarimUser> DeleteProfile(int id)
        {
            NotlarimUser user = Find(x => x.Id == id);
            
            if (user!=null)
            {
                if (Delete(user)==0)
                {
                    res.AddError(ErrorMessageCode.UserCouldNotRemove,"Kullanici Silinemedi...");
                }
            }
            else
            {
                res.AddError(ErrorMessageCode.UserCouldNotFind,"Kullanici bulunamadi.");
            }

            return res;
        }

        public new BusinessLayerResult<NotlarimUser> Insert(NotlarimUser data)
        {
            NotlarimUser user = Find(s => s.Username == data.Username || s.Email == data.Email);
            res.Result = data;

            if (user != null)
            {
                if (user.Username == data.Username)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExist, "Kullanici adi daha once kaydedilmis.");
                }
                if (user.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExist, "Email daha once kullanilmis.");
                }
            }
            else
            {
                res.Result.ProfileImageFilename = "user1.png";
                res.Result.ActivateGuid=Guid.NewGuid();

                if (base.Insert(res.Result)==0)
                {
                    res.AddError(ErrorMessageCode.UserCouldNotInserted,"Kullanici Eklenemedi");
                }
            }
            return res;
        }

        public new BusinessLayerResult<NotlarimUser> Update(NotlarimUser data)
        {
            NotlarimUser user =
                Find(x => x.Id != data.Id && (x.Username == data.Username || x.Email == data.Email));

            if (user != null && user.Id != data.Id)
            {
                if (user.Username == data.Username)
                {
                    res.AddError(ErrorMessageCode.UsernameAlreadyExist, "Bu kullanici adi daha once kullanilmis.");
                }
                if (user.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExist, "Bu e-posta daha once kullanilmis.");
                }
                return res;
            }
            res.Result = Find(s => s.Id == data.Id);
            res.Result.Email = data.Email;
            res.Result.Name = data.Name;
            res.Result.Surname = data.Surname;
            res.Result.Password = data.Password;
            res.Result.Username = data.Username;
            res.Result.IsActive = data.IsActive;
            res.Result.IsAdmin = data.IsAdmin;
           
            if (base.Update(res.Result) == 0)
            {
                res.AddError(ErrorMessageCode.UserCouldNotUpdated, "Kullanici guncellenemedi");
            }
            return res;
        }
    }
}
