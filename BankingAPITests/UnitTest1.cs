using BankingAPI.Controllers;
using BankingAPI.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

       [Test]
        public void TestGetAllInstitutions()
        {
            InstitutionController apiController = new InstitutionController();
            List<Institution> institutions = apiController.GetAllInstitutions().ToList();

            List<Institution> expectedData = new List<Institution>()
            {
                new Institution{ institutionId = 78923, name = "First Credit Union"},
                new Institution{ institutionId = 78924, name = "Bank of America"},
                new Institution{ institutionId = 78925, name = "Citi Bank"},
                new Institution{ institutionId = 2222, name = "Nikhil Kumar Talla"}
            };

            Assert.AreEqual(expectedData.Count, institutions.Count);
        }

        [Test]
        public void TestGetAllMembers()
        {
            InstitutionController apiController = new InstitutionController();
            List<Member> members = apiController.GetAllMembers().ToList();

            List<Member> expectedData = new List<Member>()
            {
                new Member  {
                            memberId = 234789
                            , givenName = "John"
                            , surname = "Doe"
                            , institutionId = 78923
                            , accounts = new List<Account>() { new Account { accountId = 23456, balance = 12.50 } }
                            }
            };

            Assert.AreEqual(expectedData.Count, members.Count);
        }
        [Test]
        public void TestGetMemberByID()
        {
            InstitutionController apiController = new InstitutionController();
            Member member = apiController.GetMemberByID(234789);

            Member expectedMember = new Member
            {
                memberId = 234789,
                givenName = "John",
                surname = "Doe",
                institutionId = 78923,
                accounts = new List<Account>() { new Account { accountId = 23456, balance = 12.50 } }
            };

            Assert.AreEqual(expectedMember.givenName, member.givenName);
        }
        [Test]
        public void TestAddMember()
        {
            InstitutionController apiController = new InstitutionController();
            Member inputMember = new Member
            {
                memberId = 111,
                givenName = "Nikhil",
                surname = "Talla",
                institutionId = 000,
                accounts = new List<Account>() { new Account { accountId = 111, balance = 222 } }
            };
            Member member = apiController.AddMember(inputMember);
            Assert.AreEqual(member.givenName, inputMember.givenName);
        }

        [Test]
        public void TestAddInstitution()
        {
            InstitutionController apiController = new InstitutionController();
            Institution inputInstitution = new Institution
            {
               institutionId = 2222, 
               name = "Nikhil Kumar Talla"
            };
            Institution institution = apiController.AddInstitution(inputInstitution);
            Assert.AreEqual(institution.name, inputInstitution.name);
        }

        [Test]
        public void TestUpdateMember()
        {
            InstitutionController apiController = new InstitutionController();
            Member inputMember = new Member
            {
                memberId = 111,
                givenName = "Nikhil Kumar",
                surname = "Talla",
                institutionId = 000,
                accounts = new List<Account>() { new Account { accountId = 111, balance = 222 } }
            };
            Member member = apiController.UpdateMember(111, inputMember);
            Assert.AreEqual(member.givenName, inputMember.givenName);
        }
        [Test]
        public void TestDeleteMember()
        {
            InstitutionController apiController = new InstitutionController();

            List<Member> deleteMembers = apiController.DeleteMember(111).ToList();
            List<Member> members = apiController.GetAllMembers().ToList();

            Assert.AreEqual(deleteMembers.Count, members.Count);
        }
        [Test]
        public void TestUpdateAccountBalance()
        {
            InstitutionController apiController = new InstitutionController();

            Account newAccountData = new Account { accountId = 1111, balance = 35.43 };
            Account updatedAccount = apiController.UpdateAccountBalance(234789, 23456, newAccountData);

            Assert.AreEqual(newAccountData.balance, updatedAccount.balance);
        }
    }
}