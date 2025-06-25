using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    roleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    roleName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Role__CD98462A27AA16B3", x => x.roleId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Test",
                columns: table => new
                {
                    testId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Test__A29BFB884D3AEFD4", x => x.testId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fullName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    passwordHash = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phoneNumber = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    gender = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    address = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    roleId = table.Column<int>(type: "int", nullable: false),
                    createAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(CURRENT_TIMESTAMP)"),
                    isActive = table.Column<bool>(type: "tinyint(1)", nullable: true, defaultValue: true),
                    avatar = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User__CB9A1CFF1AE0AA8E", x => x.userId);
                    table.ForeignKey(
                        name: "FK__User__roleId__797309D9",
                        column: x => x.roleId,
                        principalTable: "Role",
                        principalColumn: "roleId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Blog",
                columns: table => new
                {
                    blogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    content = table.Column<string>(type: "text", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    authorId = table.Column<int>(type: "int", nullable: false),
                    createAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(CURRENT_TIMESTAMP)"),
                    image = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Blog__FA0AA72DDB9CF1CE", x => x.blogId);
                    table.ForeignKey(
                        name: "FK__Blog__authorId__6477ECF3",
                        column: x => x.authorId,
                        principalTable: "User",
                        principalColumn: "userId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Consultants",
                columns: table => new
                {
                    consultantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    userId = table.Column<int>(type: "int", nullable: false),
                    degree = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    experienceYears = table.Column<int>(type: "int", nullable: true),
                    bio = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    avatar = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Consulta__8E3CA2FFC9DB1BDD", x => x.consultantId);
                    table.ForeignKey(
                        name: "FK__Consultan__userI__656C112C",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "userId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EWallet",
                columns: table => new
                {
                    walletId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    userId = table.Column<int>(type: "int", nullable: false),
                    balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    lastUpdated = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(CURRENT_TIMESTAMP)"),
                    isActive = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__EWallet__3785C8706628DB04", x => x.walletId);
                    table.ForeignKey(
                        name: "FK_EWallet_User",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "userId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MenstrualCycles",
                columns: table => new
                {
                    cyclesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    userId = table.Column<int>(type: "int", nullable: false),
                    startDate = table.Column<DateOnly>(type: "date", nullable: false),
                    endDate = table.Column<DateOnly>(type: "date", nullable: false),
                    averageLength = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Menstrua__674DB3A53DEAC6F0", x => x.cyclesId);
                    table.ForeignKey(
                        name: "FK_MenstrualCycles_User",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TestResult",
                columns: table => new
                {
                    resultId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    userId = table.Column<int>(type: "int", nullable: false),
                    testId = table.Column<int>(type: "int", nullable: false),
                    typeSTIs = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    testSample = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    testBlood = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    testUrine = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    diagnosticResults = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TestResu__C6EADC5BDFFF7F8A", x => x.resultId);
                    table.ForeignKey(
                        name: "FK__TestResul__testI__778AC167",
                        column: x => x.testId,
                        principalTable: "Test",
                        principalColumn: "testId");
                    table.ForeignKey(
                        name: "FK__TestResul__userI__787EE5A0",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "userId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserMessage",
                columns: table => new
                {
                    messageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    senderId = table.Column<int>(type: "int", nullable: false),
                    receiverId = table.Column<int>(type: "int", nullable: false),
                    message = table.Column<string>(type: "text", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    sentAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(CURRENT_TIMESTAMP)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserMess__4808B993AFBF9899", x => x.messageId);
                    table.ForeignKey(
                        name: "FK__UserMessa__recei__7A672E12",
                        column: x => x.receiverId,
                        principalTable: "User",
                        principalColumn: "userId");
                    table.ForeignKey(
                        name: "FK__UserMessa__sende__7B5B524B",
                        column: x => x.senderId,
                        principalTable: "User",
                        principalColumn: "userId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ConsultationBookings",
                columns: table => new
                {
                    consultationBookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    userId = table.Column<int>(type: "int", nullable: false),
                    consultantId = table.Column<int>(type: "int", nullable: false),
                    datetime = table.Column<DateTime>(type: "datetime", nullable: false),
                    title = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    linkConsultation = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true, defaultValue: "PENDING")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Consulta__3CF475EF4110D062", x => x.consultationBookingId);
                    table.ForeignKey(
                        name: "FK__Consultat__consu__693CA210",
                        column: x => x.consultantId,
                        principalTable: "Consultants",
                        principalColumn: "consultantId");
                    table.ForeignKey(
                        name: "FK__Consultat__userI__6A30C649",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "userId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    questionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    consultantId = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false),
                    questionText = table.Column<string>(type: "text", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    answerText = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(CURRENT_TIMESTAMP)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Question__6238D4B24EC4960E", x => x.questionId);
                    table.ForeignKey(
                        name: "FK__Question__consul__72C60C4A",
                        column: x => x.consultantId,
                        principalTable: "Consultants",
                        principalColumn: "consultantId");
                    table.ForeignKey(
                        name: "FK__Question__userId__73BA3083",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "userId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    paymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    method = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true, defaultValue: "PENDING")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    transactionTime = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(CURRENT_TIMESTAMP)"),
                    walletId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Payment__A0D9EFC6AFC573D8", x => x.paymentId);
                    table.ForeignKey(
                        name: "FK_Payment_EWallet",
                        column: x => x.walletId,
                        principalTable: "EWallet",
                        principalColumn: "walletId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OvulationReminders",
                columns: table => new
                {
                    reminderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    userId = table.Column<int>(type: "int", nullable: false),
                    cyclesId = table.Column<int>(type: "int", nullable: false),
                    reminderDate = table.Column<DateOnly>(type: "date", nullable: false),
                    type = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    note = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cycleDay = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Ovulatio__09DAAAE3D191ACC6", x => x.reminderId);
                    table.ForeignKey(
                        name: "FK_OvulationReminders_Cycles",
                        column: x => x.cyclesId,
                        principalTable: "MenstrualCycles",
                        principalColumn: "cyclesId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OvulationReminders_User",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "userId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ConsultantUserSchedule",
                columns: table => new
                {
                    scheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    userId = table.Column<int>(type: "int", nullable: false),
                    consultantId = table.Column<int>(type: "int", nullable: false),
                    consultationBookingId = table.Column<int>(type: "int", nullable: true),
                    scheduleDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    durationMinutes = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true, defaultValue: "PENDING")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    notes = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(CURRENT_TIMESTAMP)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Consulta__A532EDD40C1E81DA", x => x.scheduleId);
                    table.ForeignKey(
                        name: "FK__Consultan__consu__66603565",
                        column: x => x.consultantId,
                        principalTable: "Consultants",
                        principalColumn: "consultantId");
                    table.ForeignKey(
                        name: "FK__Consultan__consu__6754599E",
                        column: x => x.consultationBookingId,
                        principalTable: "ConsultationBookings",
                        principalColumn: "consultationBookingId");
                    table.ForeignKey(
                        name: "FK__Consultan__userI__68487DD7",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "userId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TestBooking",
                columns: table => new
                {
                    testBookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    userId = table.Column<int>(type: "int", nullable: false),
                    testId = table.Column<int>(type: "int", nullable: false),
                    scheduledDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    status = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true, defaultValue: "PENDING")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    scheduleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TestBook__383DA4EDBF983367", x => x.testBookingId);
                    table.ForeignKey(
                        name: "FK_TestBooking_Schedule",
                        column: x => x.scheduleId,
                        principalTable: "ConsultantUserSchedule",
                        principalColumn: "scheduleId");
                    table.ForeignKey(
                        name: "FK__TestBooki__testI__74AE54BC",
                        column: x => x.testId,
                        principalTable: "Test",
                        principalColumn: "testId");
                    table.ForeignKey(
                        name: "FK__TestBooki__userI__75A278F5",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "userId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    feedbackId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    userId = table.Column<int>(type: "int", nullable: false),
                    testBookingId = table.Column<int>(type: "int", nullable: true),
                    consultationBookingId = table.Column<int>(type: "int", nullable: true),
                    rating = table.Column<int>(type: "int", nullable: true),
                    comment = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    createAt = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(CURRENT_TIMESTAMP)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Feedback__2613FD24497B16D8", x => x.feedbackId);
                    table.ForeignKey(
                        name: "FK__Feedback__consul__6C190EBB",
                        column: x => x.consultationBookingId,
                        principalTable: "ConsultationBookings",
                        principalColumn: "consultationBookingId");
                    table.ForeignKey(
                        name: "FK__Feedback__testBo__6D0D32F4",
                        column: x => x.testBookingId,
                        principalTable: "TestBooking",
                        principalColumn: "testBookingId");
                    table.ForeignKey(
                        name: "FK__Feedback__userId__6E01572D",
                        column: x => x.userId,
                        principalTable: "User",
                        principalColumn: "userId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_authorId",
                table: "Blog",
                column: "authorId");

            migrationBuilder.CreateIndex(
                name: "IX_Consultants_userId",
                table: "Consultants",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultantUserSchedule_consultantId",
                table: "ConsultantUserSchedule",
                column: "consultantId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultantUserSchedule_consultationBookingId",
                table: "ConsultantUserSchedule",
                column: "consultationBookingId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultantUserSchedule_userId",
                table: "ConsultantUserSchedule",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultationBookings_consultantId",
                table: "ConsultationBookings",
                column: "consultantId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultationBookings_userId",
                table: "ConsultationBookings",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_EWallet_userId",
                table: "EWallet",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_consultationBookingId",
                table: "Feedback",
                column: "consultationBookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_testBookingId",
                table: "Feedback",
                column: "testBookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_userId",
                table: "Feedback",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_MenstrualCycles_userId",
                table: "MenstrualCycles",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_OvulationReminders_cyclesId",
                table: "OvulationReminders",
                column: "cyclesId");

            migrationBuilder.CreateIndex(
                name: "IX_OvulationReminders_userId",
                table: "OvulationReminders",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_walletId",
                table: "Payment",
                column: "walletId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_consultantId",
                table: "Question",
                column: "consultantId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_userId",
                table: "Question",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "UQ__Role__B194786163687C5D",
                table: "Role",
                column: "roleName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestBooking_scheduleId",
                table: "TestBooking",
                column: "scheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_TestBooking_testId",
                table: "TestBooking",
                column: "testId");

            migrationBuilder.CreateIndex(
                name: "IX_TestBooking_userId",
                table: "TestBooking",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_TestResult_testId",
                table: "TestResult",
                column: "testId");

            migrationBuilder.CreateIndex(
                name: "IX_TestResult_userId",
                table: "TestResult",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_User_roleId",
                table: "User",
                column: "roleId");

            migrationBuilder.CreateIndex(
                name: "UQ__User__AB6E6164CB87A5AA",
                table: "User",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserMessage_receiverId",
                table: "UserMessage",
                column: "receiverId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMessage_senderId",
                table: "UserMessage",
                column: "senderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blog");

            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "OvulationReminders");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "TestResult");

            migrationBuilder.DropTable(
                name: "UserMessage");

            migrationBuilder.DropTable(
                name: "TestBooking");

            migrationBuilder.DropTable(
                name: "MenstrualCycles");

            migrationBuilder.DropTable(
                name: "EWallet");

            migrationBuilder.DropTable(
                name: "ConsultantUserSchedule");

            migrationBuilder.DropTable(
                name: "Test");

            migrationBuilder.DropTable(
                name: "ConsultationBookings");

            migrationBuilder.DropTable(
                name: "Consultants");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
