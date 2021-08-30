using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NETCore.Model;
using NETCore.Repositoty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NETCore.Controllers
{
    [Route("api/[controller]")]     //memanggil controller
    [ApiController]
    public class OldPersonsController : ControllerBase
    {
        private readonly OldPersonRepository personRepository; //memanggil repository
        public OldPersonsController(OldPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }
        [HttpPost]
        public ActionResult Insert(Person person)
        {
            try
            {
                if (personRepository.Insert(person) > 0)
                {
                    return Ok(new
                    {
                        status = HttpStatusCode.OK,
                        data = personRepository.Get(person.NIK),
                        message = "Data berhasil DiTambahkan"
                    });


                }
                else if (personRepository.Insert(person) == 0)
                {
                    return BadRequest(new {
                        status = HttpStatusCode.BadRequest,
                        message = "NIK tidak boleh kosong" });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return BadRequest(new {
                status= HttpStatusCode.BadRequest,
                message="Data Sudah Ada"});
        }

        [HttpGet]
        public ActionResult Get(Person person)
        {

            return Ok(new
            {
                status = HttpStatusCode.OK,
                data =personRepository.Get(),
                message="Data Berhasil Ditampilkan"
            });
        }

        [HttpGet("{NIK}")]
        public ActionResult Get(string NIK)
        {

            //return Ok(personRepository.Get(NIK));
            if (personRepository.Get(NIK) != null)
            {

                return Ok(new {
                    status = HttpStatusCode.OK,
                    data =personRepository.Get(NIK),
                    message="Data Berhasil Ditampilkan"
                });
            }
            return NotFound(new
            {
                status = HttpStatusCode.NotFound,
                message = "Data yang Anda Pilih Tidak Ada"
            });
        }

        [HttpPut]

        public ActionResult Update(string Nik, Person person)
        {
            //personRepository.Update(person);
            //return Ok();
            try
            {
                if (personRepository.Update(person) != 0)
                {
                    return Ok(new
                    {
                        status = HttpStatusCode.OK,
                        data = personRepository.Get(person.NIK),
                        message = "Data berhasil Di Update"
                    });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return NotFound(new {
                status= HttpStatusCode.NotFound,
                message="Data dengan NIK Tidak Ditemukan"});
            
        }
        
        [HttpDelete("{NIK}")]
        public ActionResult Delete(string NIK)
        {
            //personRepository.Delete(NIK);
            //return Ok("Data Berhasil Dihapus");
            if (personRepository.Get(NIK) != null)
            {
                personRepository.Delete(NIK);
                return Ok(new
                {
                    status = HttpStatusCode.OK,
                    data = personRepository.Get(NIK),
                    deletedata = personRepository.Delete(NIK),
                    message = "Data berhasil Dihapus"
                });
            }
            else
            {
                return NotFound(new
                {
                    status = HttpStatusCode.NotFound,
                    message = "Data Tidak Ditemukan"
                });
            }

        }

    }
}
