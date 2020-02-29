using System;

namespace PluralsightManager.Models.Models
{
    public class ResultModel<T> where T : class
    {
        public T Data;

        public bool Ok { get; set; }
    }
}