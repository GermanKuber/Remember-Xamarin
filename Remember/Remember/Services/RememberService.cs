﻿using System.Collections.Generic;
using System.Linq;
using Remember.Data;
using Remember.Models;
using Remember.Repositories;
using Remember.Services.Interfaces;

namespace Remember.Services
{
    public class RememberService : IRememberService
    {
        private readonly IRememberRepository _rememberRepository;
        private readonly IApiService _apiService;

        private readonly INetService _netService;


        public RememberService(IRememberRepository rememberRepository, IApiService apiService, INetService netService)
        {
            _rememberRepository = rememberRepository;
            _apiService = apiService;


            _netService = netService;
        }

        public List<RememberModel> GetAll(CategoryData category, bool local = false)
        {
            var list = _rememberRepository.GetAll(category);
            var temp = new List<RememberModel>();
            if (list != null)
            {

                foreach (var rememberData in list)
                {
                    temp.Add(new RememberModel(rememberData));
                }
            }
            return temp;
        }

        public List<RememberModel> GetAllNoCompleted(CategoryData category, bool local = false)
        {
            return this.GetAll(category, local).Where(x => x.DebtCount > 0).ToList();
        }

        public void Update(RememberModel model)
        {
            if (_netService.IsConnected())
            {
                //TODO : ejecutar remoto
            }
            _rememberRepository.Update(model.RememberData);
        }

        public RememberModel GetByExactName(CategoryData category, string rememberName, bool local = false)
        {
            var modelData = _rememberRepository.GetByExactName(category, rememberName);
            if (modelData != null)
                return new RememberModel(modelData);
            else
                return null;

        }
        public List<RememberModel> GetAll(CategoryData category, string filterName, bool local = false)
        {
            if (local)
            {
                var list = _rememberRepository.GetAll(category).Where(x => x.Name.ToUpper()
                .Contains(filterName.ToUpper()))
                .OrderBy(x => x.Name)
                .ToList()?.Select(x => new RememberModel(x))?.ToList();
                return list;
            }
            else
            {
                //TODO: COnsulta remoto
                var list = _rememberRepository.GetAll(category).Where(x => x.Name.ToUpper()
                .Contains(filterName.ToUpper()))
                .OrderBy(x => x.Name)
                .ToList()?.Select(x => new RememberModel(x))?.ToList();
                return list;
            }
        }

        public List<RememberModel> GetAllNoCompleted(CategoryData category, string filterName, bool local = false)
        {
            return this.GetAll(category, filterName, local).Where(x => x.DebtCount > 0).ToList();
        }

        public Response<RememberModel> Insert(CategoryData category, RememberModel remember)
        {
            var response = _rememberRepository.Insert(category, remember.RememberData);
            if (response.IsSuccess)
                return new Response<RememberModel> { IsSuccess = true, Result = new RememberModel(response.Result) };
            else
                return new Response<RememberModel> { IsSuccess = false, Message = response.Message };

        }
    }
}