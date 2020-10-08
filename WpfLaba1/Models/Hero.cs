﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WpfLaba1.Models
{
    public class Hero: INotifyPropertyChanged
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Обязательный для ввода элемент")]
        [StringLength(50, MinimumLength = 3, ErrorMessage ="Минимум 3 символа")]
        public string Name{ get; set; }

        [Required(ErrorMessage = "Обязательный для ввода элемент")]
        [Range(1, 100,ErrorMessage ="От 1 до 100")]
        public int Hp { get; set; }

        [Required(ErrorMessage = "Обязательный для ввода элемент")]
        [Range(1, 1000, ErrorMessage = "От 1 до 1000")]
        public int Energy { get; set; }


        public string Skills { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void onPropertyChanged(string prop="")
        {
            if(PropertyChanged!=null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        private void ValibaleProperty<T>(T value, string name)
        {
            Validator.ValidateProperty(value, new ValidationContext(this, null, null)
            {
                MemberName = name
            });
        }
    }
}
