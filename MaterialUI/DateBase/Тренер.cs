//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MaterialUI.DateBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class Тренер
    {
        public byte Id { get; set; }
        public string Фамилия { get; set; }
        public string Имя { get; set; }
        public string Отчество { get; set; }
        public System.DateTime ДР { get; set; }
        public string Телефон { get; set; }
        public string Почта { get; set; }
        public string Адрес { get; set; }
        public string Паспорт { get; set; }
        public byte Пол { get; set; }
        public byte[] Фото { get; set; }
        public byte МестоРаботы { get; set; }
    
        public virtual Место_Работы Место_Работы { get; set; }
        public virtual Пол Пол1 { get; set; }
        public virtual Расписание Расписание { get; set; }
    }
}
