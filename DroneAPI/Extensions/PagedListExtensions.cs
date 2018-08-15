using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DroneAPI.Helpers;

namespace DronesApp.API.Extensions
{
    public static class PagedListExtensions
    {
        public static PagedList<TDestination> ToMappedPagedList<TSource, TDestination>(this PagedList<TSource> list)
        {
            List<TDestination> sourceList = Mapper.Map<List<TSource>, List<TDestination>>(list);
            PagedList<TDestination> pagedResult =
                new PagedList<TDestination>(sourceList, list.Count, list.CurrentPage, list.PageSize);
            return pagedResult;
        }
    }
}
