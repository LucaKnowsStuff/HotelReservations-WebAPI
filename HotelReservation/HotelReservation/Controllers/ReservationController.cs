using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
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
    public class ReservationController : ControllerBase
    {
        private readonly IUOWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<AddReservationDTO> _addReservationDTOValidator;
        private readonly IValidator<UpdateReservationDTO> _updateReservationDTOValidator;
        private const string userName = "userHardCoded";
        public ReservationController(IUOWork unitOfWork,
            IMapper mapper,
            IValidator<AddReservationDTO> addReservationDTOValidator,
            IValidator<UpdateReservationDTO> updateReservationDTOValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _addReservationDTOValidator = addReservationDTOValidator;
            _updateReservationDTOValidator = updateReservationDTOValidator;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reservationsList = await _unitOfWork.ReservationRepository.GetAll();
            return Ok(reservationsList);
        }


        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var reservation = await _unitOfWork.ReservationRepository.GetById(id);
            if (reservation == null)
            {
                return NotFound();
            }

            var reservationDTO = _mapper.Map<ReservationDTO>(reservation);
            return Ok(reservationDTO);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddReservationDTO addReservationDTO)
        {
            ValidationResult validationResult = await _addReservationDTOValidator.ValidateAsync(addReservationDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var reservation = _mapper.Map<Reservation>(addReservationDTO);
            reservation.ReservationId = Guid.NewGuid();
            reservation.IsActive = true;
            reservation.CreatedMetadata(userName);
            await _unitOfWork.ReservationRepository.Add(reservation);
            await _unitOfWork.SaveAsync();
            return Ok(addReservationDTO);
        }

        [HttpPost]
        [Route("Update/{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateReservationDTO updateReservationDTO)
        {
            ValidationResult validationResult = await _updateReservationDTOValidator.ValidateAsync(updateReservationDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var guest = await _unitOfWork.GuestRepository.GetById(updateReservationDTO.GuestId);
            if (guest == null)
            {
                return BadRequest("guest null");
            }

            if (!guest.GuestId.Equals(updateReservationDTO.GuestId))
            {
                return BadRequest("Guest Id must match the Guest id of the reservation!");
            }
            var reservation = _mapper.Map<Reservation>(updateReservationDTO);
            reservation.ReservationId = id;
            reservation = await _unitOfWork.ReservationRepository.Update(id, reservation);
            if (reservation == null)
            {
                return NotFound();
            }
            reservation.IsActive = true;
            reservation.UpdatedMetadata(userName);
            await _unitOfWork.SaveAsync();
            var reservationDTO = _mapper.Map<ReservationDTO>(reservation);
            return Ok(reservationDTO);
        }


        [HttpPost]
        [Route("Cancel/{id:Guid}")]
        public async Task<IActionResult> CancelReservation([FromRoute] Guid id)
        {
            var result = await _unitOfWork.ReservationRepository.CancelReservation(id);
            if (!result)
            {
                return NotFound();
            }
            await _unitOfWork.SaveAsync();
            return Ok();
        }


        [HttpDelete]
        [Route("MetaDelete/{id:Guid}")]
        public async Task<IActionResult> MetaDelete([FromRoute] Guid id)
        {
            var reservation = await _unitOfWork.ReservationRepository.MetaDelete(id);
            if (reservation == null)
            {
                return NotFound();
            }
            reservation.DeletedMetadata(userName);
            await _unitOfWork.SaveAsync();
            var guestDTO = _mapper.Map<ReservationDTO>(reservation);
            return Ok(guestDTO);
        }
    }
}
