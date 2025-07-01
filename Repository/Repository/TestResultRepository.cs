using EmailService.DTO;
using EmailService.Interface;
using Microsoft.EntityFrameworkCore;
using Repository.DTO;
using Repository.Models;
using Repository.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class TestResultRepository(SWP391GHSMContext _context, IEmailSender _sender)
    {
        public async Task<bool> CreateTestResult(CreateTestResult request)
        {
            try
            {
                var getBooking = await _context.TestBookings.FirstOrDefaultAsync(x => x.TestBookingId == request.BookingID);
                var createNewTestResult = new TestResult
                {
                    ResultId = Guid.NewGuid(),
                    UserId = getBooking.UserId,
                    TestId = getBooking.TestId,
                    TypeStis = request.typeSTIs ?? null,
                    TestSample = request.testSample ?? null,
                    TestBlood = request.testBlood ?? null,
                    TestUrine = request.testUrine ?? null,
                    DiagnosticResults = request.diagnosticResults ?? null,
                };

                var rawHtml = CommonUtil.HTMLLoading("TestResult.html");

                string finalHtml = rawHtml
                    .Replace("[type_stis]", createNewTestResult.TypeStis ?? "Không có")
                    .Replace("[test_sample]", createNewTestResult.TestSample ?? "Không có")
                    .Replace("[test_blood]", createNewTestResult.TestBlood ?? "Không có")
                    .Replace("[test_urine]", createNewTestResult.TestUrine ?? "Không có")
                    .Replace("[diagnostic_results]", createNewTestResult.DiagnosticResults ?? "Chưa có kết luận");

                var getUser = await _context.Users.FirstOrDefaultAsync(x => x.UserId == getBooking.UserId);

                var emailRequest = new EmailRequest<string>
                {
                    To = getUser.Email,
                    Subject = $"[Kết Quả Xét Nghiệm {createNewTestResult.TestSample}]",
                    Body = finalHtml
                };

                await _sender.SendEmailAsync(emailRequest);

                await _context.TestResults.AddAsync(createNewTestResult);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }

        public async Task<bool> EditTestResult(Guid resultId, CreateTestResult request)
        {
            try
            {
                var getTestResult = await _context.TestResults.FirstOrDefaultAsync(x => x.ResultId == resultId);
                if (getTestResult == null)
                {
                    return false;
                }

                getTestResult.TypeStis = request.typeSTIs ?? getTestResult.TypeStis;
                getTestResult.TestSample = request.testSample ?? getTestResult.TestSample;
                getTestResult.TestBlood = request.testBlood ?? getTestResult.TestBlood;
                getTestResult.TestUrine = request.testUrine ?? getTestResult.TestUrine;
                getTestResult.DiagnosticResults = request.diagnosticResults ?? getTestResult.DiagnosticResults;
                _context.TestResults.Update(getTestResult);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString(), ex);
            }
        }

        public async Task<TestResultResponse> GetTestResultByUserBooking(Guid bookingId)
        {
            var getBooking = await _context.TestBookings.FirstOrDefaultAsync(x => x.TestBookingId == bookingId);
            var getTestResultByUserBooking = await _context.TestResults.FirstOrDefaultAsync(x => x.UserId == getBooking.UserId);

            var response = new TestResultResponse
            {
                ResultId = getTestResultByUserBooking.ResultId,
                TypeSTIs = getTestResultByUserBooking.TypeStis,
                TestBlood = getTestResultByUserBooking.TestBlood,
                DiagnosticResults = getTestResultByUserBooking.DiagnosticResults,
                TestSample = getTestResultByUserBooking.TestSample,
                TestUrine = getTestResultByUserBooking.TestUrine
            };

            return response;
        }
    }
}
