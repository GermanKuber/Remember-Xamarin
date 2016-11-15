﻿using Microsoft.Practices.Unity;

namespace Remember.ViewModels
{
    public class LocatorViewModel
    {
        public LoginViewModel LoginViewModel => App.Container.Resolve<LoginViewModel>();
        public MainViewModel MainViewModel => App.Container.Resolve<MainViewModel>();
        public CategoryListViewModel CategoryListViewModel => App.Container.Resolve<CategoryListViewModel>();
        public RememberListViewModel RememberListViewModel => App.Container.Resolve<RememberListViewModel>();



    }
}
