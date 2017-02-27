﻿using System.Collections.Generic;
using System.Linq;
using Admin.Model;

namespace Admin.ViewModels
{
    public class InfoViewModel
    {
        private readonly IReadOnlyDictionary<string, IReadOnlyList<Info>> _statList;

        public IReadOnlyList<string> InfoTypeList => _statList.Keys.OrderByDescending(f => f).ToList();

        public IReadOnlyList<Info> GetSourceInfoListByType(string type) => _statList[type] ?? new List<Info>();

        public InfoViewModel(IReadOnlyDictionary<string, IReadOnlyList<Info>> statList)
        {
            _statList = statList;
        }
    }
}