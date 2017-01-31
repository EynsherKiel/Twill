using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twill.Storage.Files.Serialization;

namespace Twill.UI.Core.Models.Content.Settings
{
    public class GeneralPageModel : ViewModelBase
    {


        private ToType toType = ToType.ToDoList;
        public ToType ToType
        {
            get { return toType; }
            set { Set(ref toType, value); }
        }

    }
}
