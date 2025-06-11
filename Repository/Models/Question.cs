using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class Question
{
    public int QuestionId { get; set; }

    public int ConsultantId { get; set; }

    public int UserId { get; set; }

    public string QuestionText { get; set; } = null!;

    public string? AnswerText { get; set; }

    public DateTime? CreateAt { get; set; }

    public virtual Consultant Consultant { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
