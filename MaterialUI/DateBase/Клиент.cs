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
    
    public partial class Клиент
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Клиент()
        {
            this.К_Карта = new HashSet<К_Карта>();
            this.Посещения = new HashSet<Посещения>();
        }
    
        public int Id { get; set; }
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
        public System.DateTime ДатаРегистрации { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<К_Карта> К_Карта { get; set; }
        public virtual Пол Пол1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Посещения> Посещения { get; set; }
    }
}
