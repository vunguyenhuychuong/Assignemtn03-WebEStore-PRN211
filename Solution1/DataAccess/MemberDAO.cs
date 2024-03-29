﻿using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MemberDAO
    {
        private static MemberDAO instance = null;
        private static readonly object instanceLock = new object();
        private MemberDAO() { }
        public static MemberDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if(instance == null)
                    {
                        instance = new MemberDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Member> GetListMembers()
        {
            var list = new List<Member>();
            using (var dbContext = new SalesManagementDBContext())
            {
                list = dbContext.Members.ToList();
            }
            return list;
        }

        public Member GetMemberById(int id)
        {
            Member member1 = null;
            using (var db = new SalesManagementDBContext())
            {
                member1 = db.Members.Find(id);
            }
            return member1;
        }

        public void InsertMember(Member member)
        {
            Member check = GetMemberById(member.MemberId);
            if (check == null)
            {
                using (var db = new SalesManagementDBContext())
                {
                    db.Members.Add(new Member
                    {
                        Email = member.Email,
                        CompanyName = member.CompanyName,
                        City = member.City,
                        Country = member.Country,
                        Password = member.Password
                    });
                    //db.Members.Add(member);
                    db.SaveChanges();

                    Console.WriteLine("Save successfully");
                }
            }
            else
            {
                throw new Exception("Member exists already!");
            }
        }

        public void UpdateMember(Member member)
        {
            Member check = GetMemberById(member.MemberId);
            if (check != null)
            {
                using (var db = new SalesManagementDBContext())
                {

                    check = db.Members.Where(m => m.MemberId == member.MemberId).First();
                    check.City = member.City;
                    check.Country = member.Country;
                    check.CompanyName = member.CompanyName;
                    check.Email = member.Email;
                    check.Password = member.Password;
                    db.SaveChanges();
                }
            }
            else
            {
                throw new Exception("Member does not exist!!!");
            }
        }

        public Member Login(string email, string password)
        {
            Member loginMember = null;
            using (var db = new SalesManagementDBContext())
            {
                loginMember = db.Members.Where(member => member.Email == email && member.Password == password).SingleOrDefault();
            }
            return loginMember;
        }
    
        public void RemoveMember(int id)
        {
            Member checkMember = GetMemberById(id);
            if (checkMember != null)
            {
                using (var db = new SalesManagementDBContext())
                {
                    db.Members.Remove(checkMember);
                    db.SaveChanges();
                }
            }
            else
            {
                throw new Exception("Member does not exist!");
            }
        }
    }
}
