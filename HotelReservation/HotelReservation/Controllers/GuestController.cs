using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using HotelReservation.Data;
using HotelReservation.Helpers;
using HotelReservation.Models.Domain;
using HotelReservation.Models.DTOs;
using HotelReservation.Repositories;
using HotelReservation.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        private readonly IUOWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<AddGuestDTO> _addGuestDTOValidator;
        private readonly IValidator<UpdateGuestDTO> _updateGuestDTOValidator;
        private const string userName = "userHardCoded";
        public GuestController(IUOWork unitOfWork, IMapper mapper,
            IValidator<AddGuestDTO> addGuestDTOValidator,
            IValidator<UpdateGuestDTO> updateGuestDTOValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _addGuestDTOValidator = addGuestDTOValidator;
            _updateGuestDTOValidator = updateGuestDTOValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var guestList = await _unitOfWork.GuestRepository.GetAll();
            return Ok(guestList);
        }


        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var guest = await _unitOfWork.GuestRepository.GetById(id);
            if (guest == null)
            {
                return NotFound();
            }

            var guestDTO = _mapper.Map<GuestDTO>(guest);
            return Ok(guestDTO);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddGuestDTO addGuestDTO)
        {
            ValidationResult validationResult = await _addGuestDTOValidator.ValidateAsync(addGuestDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var guest = new Guest
            {
                GuestId = Guid.NewGuid(),
                GuestName = addGuestDTO.GuestName,
                GuestPhoneNumber = addGuestDTO.GuestPhoneNumber,
                GuestPreferences = addGuestDTO.GuestPreferences,
            };
            guest.CreatedMetadata(userName);
            await _unitOfWork.GuestRepository.Add(guest);
            await _unitOfWork.SaveAsync();
            return Ok(addGuestDTO);
        }

        [HttpPost]
        [Route("Update/{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateGuestDTO updateGuestDTO)
        {
            ValidationResult validationResult = await _updateGuestDTOValidator.ValidateAsync(updateGuestDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var guest = _mapper.Map<Guest>(updateGuestDTO);
            guest.GuestId = id;
            guest = await _unitOfWork.GuestRepository.Update(id, guest);
            if (guest == null)
            {
                return NotFound();
            }
            guest.UpdatedMetadata(userName);
            await _unitOfWork.SaveAsync();
            var guestDTO = _mapper.Map<GuestDTO>(guest);
            return Ok(guestDTO);
        }

        [HttpDelete]
        [Route("Delete/{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var guest = await _unitOfWork.GuestRepository.Delete(id);
            if (guest == null)
            {
                return NotFound();
            }
            await _unitOfWork.SaveAsync();
            var guestDTO = _mapper.Map<GuestDTO>(guest);
            return Ok(guestDTO);
        }


        [HttpDelete]
        [Route("MetaDelete/{id:Guid}")]
        public async Task<IActionResult> MetaDelete([FromRoute] Guid id)
        {
            var guest = await _unitOfWork.GuestRepository.MetaDelete(id);
            if (guest == null)
            {
                return NotFound();
            }
            guest.DeletedMetadata(userName);
            await _unitOfWork.SaveAsync();
            var guestDTO = _mapper.Map<GuestDTO>(guest);
            return Ok(guestDTO);
        }
    }
}
