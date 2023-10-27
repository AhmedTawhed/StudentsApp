﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using StudentsApp.Validations;

namespace StudentsApp.Models;

[Index("Code", Name = "UQ__Subjects__A25C5AA7449983D6", IsUnique = true)]
public partial class Subject
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Range(2000, int.MaxValue)]
    [UniqueSubjectCode]
    public int Code { get; set; }

    [Required]
    [StringLength(25)]
    [MinLength(3)]
    [Unicode(false)]
    public string Name { get; set; }

    public bool? IsDeleted { get; set; } = false;

    [InverseProperty("Subject")]
    public virtual ICollection<StudentSubject> StudentSubjects { get; set; } = new List<StudentSubject>();
}