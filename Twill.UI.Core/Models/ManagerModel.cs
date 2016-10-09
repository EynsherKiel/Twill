﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Twill.UI.Core.Models.Tray;
using Twill.UI.Core.Tools;

namespace Twill.UI.Core.Models
{
    public class ManagerModel : Notify
    {
        public ManagerModel()
        {
            if (IconBehaviorModel.TrayPopupModel != null)
                IconBehaviorModel.TrayPopupModel.MenuModel = MenuModel;
        }

        public MenuModel MenuModel { get; set; } = new MenuModel();
        public IconBehaviorModel IconBehaviorModel { get; set; } = new IconBehaviorModel();

        
    }
}
