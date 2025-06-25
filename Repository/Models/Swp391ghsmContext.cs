using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace Repository.Models;

public partial class SWP391GHSMContext : DbContext
{
    public SWP391GHSMContext()
    {
    }

    public SWP391GHSMContext(DbContextOptions<SWP391GHSMContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<Consultant> Consultants { get; set; }

    public virtual DbSet<ConsultantUserSchedule> ConsultantUserSchedules { get; set; }

    public virtual DbSet<ConsultationBooking> ConsultationBookings { get; set; }

    public virtual DbSet<Ewallet> Ewallets { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<MenstrualCycle> MenstrualCycles { get; set; }

    public virtual DbSet<OvulationReminder> OvulationReminders { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Test> Tests { get; set; }

    public virtual DbSet<TestBooking> TestBookings { get; set; }

    public virtual DbSet<TestResult> TestResults { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserMessage> UserMessages { get; set; }

    public static string GetConnectionString(string connectionStringName)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        string connectionString = config.GetConnectionString(connectionStringName);
        return connectionString;
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseSqlServer(GetConnectionString("DefaultConnection")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.BlogId).HasName("PK__Blog__FA0AA72DDB9CF1CE");

            entity.ToTable("Blog");

            entity.Property(e => e.BlogId).HasColumnName("blogId");
            entity.Property(e => e.AuthorId).HasColumnName("authorId");
            entity.Property(e => e.Content)
                .HasColumnType("text")
                .HasColumnName("content");
            entity.Property(e => e.CreateAt)
                .HasDefaultValueSql("(CURRENT_TIMESTAMP)")
                .HasColumnType("datetime")
                .HasColumnName("createAt");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("image");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("title");

            entity.HasOne(d => d.Author).WithMany(p => p.Blogs)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Blog__authorId__6477ECF3");
        });

        modelBuilder.Entity<Consultant>(entity =>
        {
            entity.HasKey(e => e.ConsultantId).HasName("PK__Consulta__8E3CA2FFC9DB1BDD");

            entity.Property(e => e.ConsultantId).HasColumnName("consultantId");
            entity.Property(e => e.Avatar)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("avatar");
            entity.Property(e => e.Bio)
                .HasColumnType("text")
                .HasColumnName("bio");
            entity.Property(e => e.Degree)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("degree");
            entity.Property(e => e.ExperienceYears).HasColumnName("experienceYears");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Consultants)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Consultan__userI__656C112C");
        });

        modelBuilder.Entity<ConsultantUserSchedule>(entity =>
        {
            entity.HasKey(e => e.ScheduleId).HasName("PK__Consulta__A532EDD40C1E81DA");

            entity.ToTable("ConsultantUserSchedule");

            entity.Property(e => e.ScheduleId).HasColumnName("scheduleId");
            entity.Property(e => e.ConsultantId).HasColumnName("consultantId");
            entity.Property(e => e.ConsultationBookingId).HasColumnName("consultationBookingId");
            entity.Property(e => e.CreateAt)
                .HasDefaultValueSql("(CURRENT_TIMESTAMP)")
                .HasColumnType("datetime")
                .HasColumnName("createAt");
            entity.Property(e => e.DurationMinutes).HasColumnName("durationMinutes");
            entity.Property(e => e.Notes)
                .HasColumnType("text")
                .HasColumnName("notes");
            entity.Property(e => e.ScheduleDateTime)
                .HasColumnType("datetime")
                .HasColumnName("scheduleDateTime");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("PENDING")
                .HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Consultant).WithMany(p => p.ConsultantUserSchedules)
                .HasForeignKey(d => d.ConsultantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Consultan__consu__66603565");

            entity.HasOne(d => d.ConsultationBooking).WithMany(p => p.ConsultantUserSchedules)
                .HasForeignKey(d => d.ConsultationBookingId)
                .HasConstraintName("FK__Consultan__consu__6754599E");

            entity.HasOne(d => d.User).WithMany(p => p.ConsultantUserSchedules)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Consultan__userI__68487DD7");
        });

        modelBuilder.Entity<ConsultationBooking>(entity =>
        {
            entity.HasKey(e => e.ConsultationBookingId).HasName("PK__Consulta__3CF475EF4110D062");

            entity.Property(e => e.ConsultationBookingId).HasColumnName("consultationBookingId");
            entity.Property(e => e.ConsultantId).HasColumnName("consultantId");
            entity.Property(e => e.Datetime)
                .HasColumnType("datetime")
                .HasColumnName("datetime");
            entity.Property(e => e.LinkConsultation)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("linkConsultation");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("PENDING")
                .HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Consultant).WithMany(p => p.ConsultationBookings)
                .HasForeignKey(d => d.ConsultantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Consultat__consu__693CA210");

            entity.HasOne(d => d.User).WithMany(p => p.ConsultationBookings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Consultat__userI__6A30C649");
        });

        modelBuilder.Entity<Ewallet>(entity =>
        {
            entity.HasKey(e => e.WalletId).HasName("PK__EWallet__3785C8706628DB04");

            entity.ToTable("EWallet");

            entity.Property(e => e.WalletId).HasColumnName("walletId");
            entity.Property(e => e.Balance)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("balance");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("isActive");
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("(CURRENT_TIMESTAMP)")
                .HasColumnType("datetime")
                .HasColumnName("lastUpdated");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Ewallets)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EWallet_User");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Feedback__2613FD24497B16D8");

            entity.ToTable("Feedback");

            entity.Property(e => e.FeedbackId).HasColumnName("feedbackId");
            entity.Property(e => e.Comment)
                .HasColumnType("text")
                .HasColumnName("comment");
            entity.Property(e => e.ConsultationBookingId).HasColumnName("consultationBookingId");
            entity.Property(e => e.CreateAt)
                .HasDefaultValueSql("(CURRENT_TIMESTAMP)")
                .HasColumnType("datetime")
                .HasColumnName("createAt");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.TestBookingId).HasColumnName("testBookingId");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.ConsultationBooking).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.ConsultationBookingId)
                .HasConstraintName("FK__Feedback__consul__6C190EBB");

            entity.HasOne(d => d.TestBooking).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.TestBookingId)
                .HasConstraintName("FK__Feedback__testBo__6D0D32F4");

            entity.HasOne(d => d.User).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Feedback__userId__6E01572D");
        });

        modelBuilder.Entity<MenstrualCycle>(entity =>
        {
            entity.HasKey(e => e.CyclesId).HasName("PK__Menstrua__674DB3A53DEAC6F0");

            entity.Property(e => e.CyclesId).HasColumnName("cyclesId");
            entity.Property(e => e.AverageLength).HasColumnName("averageLength");
            entity.Property(e => e.EndDate).HasColumnName("endDate");
            entity.Property(e => e.StartDate).HasColumnName("startDate");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.MenstrualCycles)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_MenstrualCycles_User");
        });

        modelBuilder.Entity<OvulationReminder>(entity =>
        {
            entity.HasKey(e => e.ReminderId).HasName("PK__Ovulatio__09DAAAE3D191ACC6");

            entity.Property(e => e.ReminderId).HasColumnName("reminderId");
            entity.Property(e => e.CycleDay).HasColumnName("cycleDay");
            entity.Property(e => e.CyclesId).HasColumnName("cyclesId");
            entity.Property(e => e.Note)
                .HasMaxLength(255)
                .HasColumnName("note");
            entity.Property(e => e.ReminderDate).HasColumnName("reminderDate");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Cycles).WithMany(p => p.OvulationReminders)
                .HasForeignKey(d => d.CyclesId)
                .HasConstraintName("FK_OvulationReminders_Cycles");

            entity.HasOne(d => d.User).WithMany(p => p.OvulationReminders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OvulationReminders_User");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__A0D9EFC6AFC573D8");

            entity.ToTable("Payment");

            entity.Property(e => e.PaymentId).HasColumnName("paymentId");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("amount");
            entity.Property(e => e.Method)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("method");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("PENDING")
                .HasColumnName("status");
            entity.Property(e => e.TransactionTime)
                .HasDefaultValueSql("(CURRENT_TIMESTAMP)")
                .HasColumnType("datetime")
                .HasColumnName("transactionTime");
            entity.Property(e => e.WalletId).HasColumnName("walletId");

            entity.HasOne(d => d.Wallet).WithMany(p => p.Payments)
                .HasForeignKey(d => d.WalletId)
                .HasConstraintName("FK_Payment_EWallet");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__Question__6238D4B24EC4960E");

            entity.ToTable("Question");

            entity.Property(e => e.QuestionId).HasColumnName("questionId");
            entity.Property(e => e.AnswerText)
                .HasColumnType("text")
                .HasColumnName("answerText");
            entity.Property(e => e.ConsultantId).HasColumnName("consultantId");
            entity.Property(e => e.CreateAt)
                .HasDefaultValueSql("(CURRENT_TIMESTAMP)")
                .HasColumnType("datetime")
                .HasColumnName("createAt");
            entity.Property(e => e.QuestionText)
                .HasColumnType("text")
                .HasColumnName("questionText");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Consultant).WithMany(p => p.Questions)
                .HasForeignKey(d => d.ConsultantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Question__consul__72C60C4A");

            entity.HasOne(d => d.User).WithMany(p => p.Questions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Question__userId__73BA3083");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__CD98462A27AA16B3");

            entity.ToTable("Role");

            entity.HasIndex(e => e.RoleName, "UQ__Role__B194786163687C5D").IsUnique();

            entity.Property(e => e.RoleId).HasColumnName("roleId");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("roleName");
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.HasKey(e => e.TestId).HasName("PK__Test__A29BFB884D3AEFD4");

            entity.ToTable("Test");

            entity.Property(e => e.TestId).HasColumnName("testId");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
        });

        modelBuilder.Entity<TestBooking>(entity =>
        {
            entity.HasKey(e => e.TestBookingId).HasName("PK__TestBook__383DA4EDBF983367");

            entity.ToTable("TestBooking");

            entity.Property(e => e.TestBookingId).HasColumnName("testBookingId");
            entity.Property(e => e.ScheduleId).HasColumnName("scheduleId");
            entity.Property(e => e.ScheduledDate)
                .HasColumnType("datetime")
                .HasColumnName("scheduledDate");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("PENDING")
                .HasColumnName("status");
            entity.Property(e => e.TestId).HasColumnName("testId");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Schedule).WithMany(p => p.TestBookings)
                .HasForeignKey(d => d.ScheduleId)
                .HasConstraintName("FK_TestBooking_Schedule");

            entity.HasOne(d => d.Test).WithMany(p => p.TestBookings)
                .HasForeignKey(d => d.TestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TestBooki__testI__74AE54BC");

            entity.HasOne(d => d.User).WithMany(p => p.TestBookings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TestBooki__userI__75A278F5");
        });

        modelBuilder.Entity<TestResult>(entity =>
        {
            entity.HasKey(e => e.ResultId).HasName("PK__TestResu__C6EADC5BDFFF7F8A");

            entity.ToTable("TestResult");

            entity.Property(e => e.ResultId).HasColumnName("resultId");
            entity.Property(e => e.DiagnosticResults)
                .HasColumnType("text")
                .HasColumnName("diagnosticResults");
            entity.Property(e => e.TestBlood)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("testBlood");
            entity.Property(e => e.TestId).HasColumnName("testId");
            entity.Property(e => e.TestSample)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("testSample");
            entity.Property(e => e.TestUrine)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("testUrine");
            entity.Property(e => e.TypeStis)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("typeSTIs");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.Test).WithMany(p => p.TestResults)
                .HasForeignKey(d => d.TestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TestResul__testI__778AC167");

            entity.HasOne(d => d.User).WithMany(p => p.TestResults)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TestResul__userI__787EE5A0");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__CB9A1CFF1AE0AA8E");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "UQ__User__AB6E6164CB87A5AA").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Avatar)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("avatar");
            entity.Property(e => e.CreateAt)
                .HasDefaultValueSql("(CURRENT_TIMESTAMP)")
                .HasColumnType("datetime")
                .HasColumnName("createAt");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("fullName");
            entity.Property(e => e.Gender)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("gender");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("isActive");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("passwordHash");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phoneNumber");
            entity.Property(e => e.RoleId).HasColumnName("roleId");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__User__roleId__797309D9");
        });

        modelBuilder.Entity<UserMessage>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__UserMess__4808B993AFBF9899");

            entity.ToTable("UserMessage");

            entity.Property(e => e.MessageId).HasColumnName("messageId");
            entity.Property(e => e.Message)
                .HasColumnType("text")
                .HasColumnName("message");
            entity.Property(e => e.ReceiverId).HasColumnName("receiverId");
            entity.Property(e => e.SenderId).HasColumnName("senderId");
            entity.Property(e => e.SentAt)
                .HasDefaultValueSql("(CURRENT_TIMESTAMP)")
                .HasColumnType("datetime")
                .HasColumnName("sentAt");

            entity.HasOne(d => d.Receiver).WithMany(p => p.UserMessageReceivers)
                .HasForeignKey(d => d.ReceiverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserMessa__recei__7A672E12");

            entity.HasOne(d => d.Sender).WithMany(p => p.UserMessageSenders)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserMessa__sende__7B5B524B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
