using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BankingAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BankingAPI.Controllers
{
    
    [ApiController]
    public class InstitutionController : ControllerBase
    {
        [HttpGet]
        [Route("api/Institution/GetAllInstitutions")]
        public IEnumerable<Institution> GetAllInstitutions()
        {
            string rawJSON = System.IO.File.ReadAllText(@"D:\Coding\BankJoy\BankingAPI\BankingAPI\database.json");
            DataCollection data = JsonConvert.DeserializeObject<DataCollection>(rawJSON);
            return data.Institutions;
        }

        [HttpPost]
        [Route("api/Institution/AddInstitution")]
        public Institution AddInstitution([FromBody] Institution institution)
        {
            try
            {
                string rawJSON = System.IO.File.ReadAllText(@"D:\Coding\BankJoy\BankingAPI\BankingAPI\database.json");
                DataCollection data = JsonConvert.DeserializeObject<DataCollection>(rawJSON);
                data.Institutions.Add(institution);

                /* Updating the database.json file with newly added Institution */
                var finalJson = JsonConvert.SerializeObject(data, Formatting.Indented);
                System.IO.File.WriteAllText(@"D:\Coding\BankJoy\BankingAPI\BankingAPI\database.json", finalJson.ToString());

                //var message = Request.CreateResponse(HttpStatusCode.Created, institution);
                //message.Headers.Location = new Uri(Request.RequestUri + "?id=" + institution.institutionId);
                return institution;
            }
            catch (Exception ex)
            {
                //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                return institution;
            }
        }

        [HttpGet]
        [Route("api/Member/GetAllMembers")]
        public IEnumerable<Member> GetAllMembers()
        {
            string rawJSON = System.IO.File.ReadAllText(@"D:\Coding\BankJoy\BankingAPI\BankingAPI\database.json");
            DataCollection data = JsonConvert.DeserializeObject<DataCollection>(rawJSON);
            return data.Members;
        }

        [HttpGet]
        [Route("api/Member/GetMemberByID")]
        public Member GetMemberByID(int id)
        {
            string rawJSON = System.IO.File.ReadAllText(@"D:\Coding\BankJoy\BankingAPI\BankingAPI\database.json");
            DataCollection data = JsonConvert.DeserializeObject<DataCollection>(rawJSON);

            if (data != null && data.Members.Count > 0)
            {
                var member = data.Members.FirstOrDefault(x => x.memberId == id);
                if (member != null)
                {
                    return member;
                }
            }
            //string errorMsg = Request.CreateErrorResponse(HttpStatusCode.NotFound, "Member with ID = " + id.ToString() + " was not found ").ToString();

            return null;
        }

        [HttpPost]
        [Route("api/Member/AddMember")]
        public Member AddMember([FromBody] Member member)
        {
            try
            {
                string rawJSON = System.IO.File.ReadAllText(@"D:\Coding\BankJoy\BankingAPI\BankingAPI\database.json");
                DataCollection data = JsonConvert.DeserializeObject<DataCollection>(rawJSON);
                data.Members.Add(member);

                /* Updating the database.json file with newly added member */
                var finalJson = JsonConvert.SerializeObject(data, Formatting.Indented);
                System.IO.File.WriteAllText(@"D:\Coding\BankJoy\BankingAPI\BankingAPI\database.json", finalJson.ToString());

                //var message = Request.CreateResponse(HttpStatusCode.Created, member);
                //message.Headers.Location = new Uri(Request.RequestUri + "?id=" + member.memberId);
                return member;

            }
            catch (Exception ex)
            {
                return member;
                //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPut]
        [Route("api/Member/UpdateMember")]
        public Member UpdateMember(int id, [FromBody] Member member)
        {
            try
            {
                string rawJSON = System.IO.File.ReadAllText(@"D:\Coding\BankJoy\BankingAPI\BankingAPI\database.json");
                DataCollection data = JsonConvert.DeserializeObject<DataCollection>(rawJSON);

                var entity = (data != null && data.Members.Count > 0) ? (data.Members.FirstOrDefault(x => x.memberId == id)) : null;
                if (entity != null)
                {
                    entity = member;
                    /* Updating the database.json file with updated member data*/
                    var finalJson = JsonConvert.SerializeObject(data, Formatting.Indented);
                    System.IO.File.WriteAllText(@"D:\Coding\BankJoy\BankingAPI\BankingAPI\database.json", finalJson.ToString());

                    //var message = Request.CreateResponse(HttpStatusCode.OK, entity);
                    return entity;
                }
                else
                {
                    return member;
                    //return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Member with ID = " + id.ToString() + " is not found");
                }
            }
            catch (Exception ex)
            {
                //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                return member;
            }
        }

        [HttpDelete]
        [Route("api/Member/DeleteMember")]
        public IEnumerable<Member> DeleteMember(int id)
        {
            string rawJSON = System.IO.File.ReadAllText(@"D:\Coding\BankJoy\BankingAPI\BankingAPI\database.json");
            DataCollection data = JsonConvert.DeserializeObject<DataCollection>(rawJSON);

            try
            {
                var entity = (data != null && data.Members.Count > 0) ? (data.Members.FirstOrDefault(x => x.memberId == id)) : null;
                if (entity != null)
                {
                    data.Members.Remove(entity);
                    /* Updating the database.json file with updated member data*/
                    var finalJson = JsonConvert.SerializeObject(data, Formatting.Indented);
                    System.IO.File.WriteAllText(@"D:\Coding\BankJoy\BankingAPI\BankingAPI\database.json", finalJson.ToString());

                    //var message = Request.CreateResponse(HttpStatusCode.OK);
                    return data.Members;
                }
                else
                {
                    return data.Members;
                    //return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Member with ID = " + id.ToString() + " was not found");
                }
            }
            catch (Exception ex)
            {
                return data.Members;
                //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPut]
        [Route("api/Member/UpdateAccountBalance")]
        public Account UpdateAccountBalance(int memberid, int accountID, [FromBody] Account newAccountData)
        {
            try
            {
                string rawJSON = System.IO.File.ReadAllText(@"D:\Coding\BankJoy\BankingAPI\BankingAPI\database.json");
                DataCollection data = JsonConvert.DeserializeObject<DataCollection>(rawJSON);
                Member member = (data != null && data.Members.Count > 0) ? (data.Members.FirstOrDefault(x => x.memberId == memberid)) : null;
                Account account = (member != null && member.accounts.Count > 0) ? (member.accounts.FirstOrDefault(x => x.accountId == accountID)) : null;
                if (account != null)
                {
                    account = newAccountData;
                    /* Updating the database.json file with updated account data*/
                    var finalJson = JsonConvert.SerializeObject(data, Formatting.Indented);
                    System.IO.File.WriteAllText(@"D:\Coding\BankJoy\BankingAPI\BankingAPI\database.json", finalJson.ToString());

                    //var message = Request.CreateResponse(HttpStatusCode.OK, account);
                    return account;
                }
                else
                {
                    return newAccountData;
                    //return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Member with Account ID = " + accountID.ToString() + " is not found");
                }
            }
            catch (Exception ex)
            {
                //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                return newAccountData;
            }
        }

        //[HttpPut]
        //[Route("api/Member/UpdateTransferAccountBalance")]
        //public HttpResponseMessage UpdateTransferAccountBalance(int institutionId, [FromBody] Account newAccountData)
        //{
        //    try
        //    {
        //        string rawJSON = System.IO.File.ReadAllText(@"D:\Coding\BankJoy\BankingAPI\BankingAPI\database.json");
        //        DataCollection data = JsonConvert.DeserializeObject<DataCollection>(rawJSON);
        //        IEnumerable<Member> members = (data != null && data.Members.Count > 0) ? (data.Members.Where(x => x.institutionId == institutionId)) : null;
        //        if (account != null)
        //        {
        //            account = newAccountData;
        //            /* Updating the database.json file with updated account data*/
        //            var finalJson = JsonConvert.SerializeObject(data, Formatting.Indented);
        //            System.IO.File.WriteAllText(@"D:\Coding\BankJoy\BankingAPI\BankingAPI\database.json", finalJson.ToString());

        //            var message = Request.CreateResponse(HttpStatusCode.OK, account);
        //            return message;
        //        }
        //        else
        //        {
        //            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No data is not found");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        //    }
        //}
    }
}