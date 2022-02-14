using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Notlarim102.WebApp.ViewModel
{
    public class OkViewModel:NotifyViewModelBase<string>
    {
        public OkViewModel()
        {
            Title = "Islem basarili";
        }
    }
}