using ApexTournamentManager.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexTournamentManager.Core
{
    static class ListSearchHelper
    {
        public static basicData FindObjectById(this ObservableCollection<basicData> coll, Guid id)
        {
            foreach (basicData obj in coll)
            {
                if (obj.id == id)
                {
                    return obj;
                }
            }
            return null;
        }

        public static basicData FindObjectByName(this ObservableCollection<basicData> coll, string name)
        {
            foreach (basicData obj in coll)
            {
                if (obj.name == name)
                {
                    return obj;
                }
            }
            return null;
        }

        public static T ElementAtOrLast<T>(this ObservableCollection<T> list, int index)
        {
            T obj = list.ElementAtOrDefault(index);
            if(obj != null)
                return obj;
            else return list.Last();
        }
    }
}
