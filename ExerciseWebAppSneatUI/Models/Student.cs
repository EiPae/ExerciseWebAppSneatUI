using System;
using System.Collections.Generic;

namespace ExerciseWebAppSneatUI.Models;

public partial class Student
{
    public int StudentNo { get; set; }

    public string? StudentName { get; set; }

    public string? Course { get; set; }

    public int? Mark { get; set; }

    public string? Grade { get; set; }
}
