using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DevOpsManager.MobileApp.Components
{
    public class SimpleListViewNo : StackLayout
    {

        //public static BindableProperty ItemSourceProperty = BindableProperty.Create(nameof(ItemSource),
        //                                                                            typeof(IEnumerable<object>),
        //                                                                            typeof(SimpleListView),
        //                                                                            propertyChanged: OnItemSourceChanged);
        //public static BindableProperty ItemTemplateProperty = BindableProperty.Create(nameof(ItemTemplate),
        //                                                                              typeof(DataTemplate),
        //                                                                              typeof(SimpleListView),
        //                                                                              propertyChanged: OnItemTemplateChanged);

        //public IEnumerable<object> ItemSource { get; set; }

        //public DataTemplate ItemTemplate { get; set; }

        //private void Compute()
        //{
        //    if (ItemSource != null && ItemTemplate != null)
        //    {
        //        Children.Clear();
        //        View content = (View) ItemTemplate.CreateContent();
        //        content.BindingContext = ItemSource;
        //        Children.Add(content);
        //    }
        //}

        //private static void OnItemSourceChanged(BindableObject bindable, object oldValue, object newValue)
        //{
        //    var view = (SimpleListView)bindable;
        //    view.ItemSource = (IEnumerable<object>)newValue;
        //    view.Compute();
        //}

        //private static void OnItemTemplateChanged(BindableObject bindable, object oldValue, object newValue)
        //{
        //    var view = (SimpleListView)bindable;
        //    view.ItemTemplate = (DataTemplate)newValue;
        //    view.Compute();
        //}

    }
}
