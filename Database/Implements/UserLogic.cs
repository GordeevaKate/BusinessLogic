
using DatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using КурсоваяBusinessLogic.Interfaces;
using КурсоваяBusinessLogic.BindingModel;
using КурсоваяBusinessLogic.ViewModel;

namespace DatabaseImplement.Implements
{
    public class UserLogic : IUserLogic
    {
        public void CreateOrUpdate(UserBindingModel model)
        {
            using (var context = new KursachDatabase())
            {
                User element = model.Id.HasValue ? null : new User();
                if (model.Id.HasValue)
                {
                    element = context.Users.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new User();
                    context.Users.Add(element);
                }
                element.Status = model.Status;
                element.Login = model.Login;
                element.Password = model.Password;
                context.SaveChanges();
            }
        }
        public void Delete(UserBindingModel model)
        {
            using (var context = new KursachDatabase())
            {
                User element = context.Users.FirstOrDefault(rec => rec.Id == model.Id);

                if (element != null)
                {
                    context.Users.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        public List<UserViewModel> Read(UserBindingModel model)
        {
            using (var context = new KursachDatabase())
            {
                return context.Users
                 .Where(rec => model == null
                   || (rec.Login == model.Login &&rec.Password == model.Password))
               .Select(rec => new UserViewModel
               {
                   Id = rec.Id,
                   Login = rec.Login,
                   Password = rec.Password,
                   Status=rec.Status
               })
                .ToList();
            }
        }
       
    }
}