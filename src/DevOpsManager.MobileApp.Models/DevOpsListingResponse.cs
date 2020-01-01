using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace DevOpsManager.MobileApp.Models
{
    public class DevOpsListingResponse<T>
    {

        public IEnumerable<T> Value { get; set; }

        public int Count { get; set; }

        public static implicit operator ObservableCollection<T>(DevOpsListingResponse<T> val)
            => new ObservableCollection<T>(val.Value);

        public static implicit operator List<T>(DevOpsListingResponse<T> val)
                => val.Value.ToList();

        public static implicit operator T[](DevOpsListingResponse<T> val)
                => val.Value.ToArray();

    }
}
