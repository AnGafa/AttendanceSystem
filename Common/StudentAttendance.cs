//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Common
{
    using System;
    using System.Collections.Generic;
    
    public partial class StudentAttendance
    {
        public int AttendanceID { get; set; }
        public int LessonFK { get; set; }
        public bool Presence { get; set; }
        public int StudentFK { get; set; }
    
        public virtual Lesson Lesson { get; set; }
        public virtual Student Student { get; set; }
    }
}
