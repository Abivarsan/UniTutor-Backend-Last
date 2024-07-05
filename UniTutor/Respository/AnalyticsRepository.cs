﻿
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UniTutor.Data;
using UniTutor.DTO;
using UniTutor.Interface;
using UniTutor.Models;

namespace UniTutor.Repository
{
    public class AnalyticsRepository : IAnalytics
    {
        private readonly ApplicationDbContext _context;

        public AnalyticsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<WeeklyDataDto>> GetWeeklyJoinedTutorsAsync()
        {
            return await GetWeeklyDataAsync(_context.Tutors, t => t.CreatedAt);
        }

        public async Task<List<WeeklyDataDto>> GetWeeklyJoinedStudentsAsync()
        {
            return await GetWeeklyDataAsync(_context.Students, s => s.CreatedAt);
        }

        public async Task<List<WeeklyDataDto>> GetWeeklyTutorRequestsAsync()
        {
            return await GetWeeklyDataAsync(_context.TutorRequests, r => r.RequestDate);
        }

        public async Task<List<WeeklyDataDto>> GetWeeklyCommentsAsync()
        {
            return await GetWeeklyDataAsync(_context.Comments, c => c.Date);
        }

        private async Task<List<WeeklyDataDto>> GetWeeklyDataAsync<T>(IQueryable<T> query, Expression<Func<T, DateTime>> dateSelector)
            where T : class
        {
            var startDate = DateTime.Now.AddDays(-6).Date;

            var data = await query
                .Where(BuildDateFilterExpression(dateSelector, startDate))
                .ToListAsync();

            var groupedData = data
                .GroupBy(d => dateSelector.Compile()(d).DayOfWeek)
                .Select(g => new WeeklyDataDto
                {
                    Day = g.Key.ToString(),
                    Count = g.Count()
                })
                .ToList();

            return groupedData;
        }

        private static Expression<Func<T, bool>> BuildDateFilterExpression<T>(Expression<Func<T, DateTime>> dateSelector, DateTime startDate)
        {
            var parameter = dateSelector.Parameters[0];
            var body = Expression.GreaterThanOrEqual(dateSelector.Body, Expression.Constant(startDate));
            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
    }
}