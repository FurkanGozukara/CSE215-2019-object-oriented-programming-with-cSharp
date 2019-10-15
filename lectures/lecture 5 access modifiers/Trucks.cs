using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lecture_5_access_modifiers
{

    //cars : Super Class: The class whose features are inherited is known as super class(or a base class or a parent class).
    //trucks : Sub Class: The class that inherits the other class is known as subclass(or a derived class, extended class, or child class). The subclass can add its own fields and methods in addition to the superclass fields and methods.
    public class Trucks : Cars
    {
        public int capacity { get; set; }

        public void setModel(string srModel)
        {
            this.model = srModel;
        }
    }
}
