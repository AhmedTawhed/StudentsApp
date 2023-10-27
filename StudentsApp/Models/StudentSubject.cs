﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace StudentsApp.Models;

[PrimaryKey("StudentId", "SubjectId")]
public partial class StudentSubject
{
    [Key]
    public Guid StudentId { get; set; }

    [Key]
    public Guid SubjectId { get; set; }
    [Range(0, 100)]
    public int? Grade { get; set; }

    [ForeignKey("StudentId")]
    [InverseProperty("StudentSubjects")]
    public virtual Student Student { get; set; }

    [ForeignKey("SubjectId")]
    [InverseProperty("StudentSubjects")]
    public virtual Subject Subject { get; set; }
}