using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using HotelReservation.Helpers;
using HotelReservation.Models.Domain;
using HotelReservation.Models.DTOs;
using HotelReservation.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IUOWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<AddRoomDTO> _addRoomDTOValidator;
        private readonly IValidator<UpdateRoomDTO> _updateRoomDTOValidator;
        private const string userName = "userHardCoded";
        public RoomController(IUOWork unitOfWork,
            IMapper mapper,
            IValidator<AddRoomDTO> addRoomDTOValidator,
            IValidator<UpdateRoomDTO> updateRoomDTOValidator)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            _addRoomDTOValidator = addRoomDTOValidator;
            _updateRoomDTOValidator = updateRoomDTOValidator;
        }



        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roomList = await _unitOfWork.RoomRepository.GetAll();
            return Ok(roomList);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var room = await _unitOfWork.RoomRepository.GetById(id);
            if (room == null)
            {
                return NotFound();
            }

            var roomDTO = _mapper.Map<RoomDTO>(room);
            return Ok(roomDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRoomDTO addRoomDTO)
        {
            ValidationResult validationResult = await _addRoomDTOValidator.ValidateAsync(addRoomDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var room = _mapper.Map<Room>(addRoomDTO);
            room.RoomId = Guid.NewGuid();
            room.CreatedMetadata(userName);
            await _unitOfWork.RoomRepository.Add(room);
            await _unitOfWork.SaveAsync();
            return Ok(addRoomDTO);
        }

        [HttpPost]
        [Route("Update/{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRoomDTO updateRoomDTO)
        {
            ValidationResult validationResult = await _updateRoomDTOValidator.ValidateAsync(updateRoomDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            var room = _mapper.Map<Room>(updateRoomDTO);
            room.RoomId = id;
            room = await _unitOfWork.RoomRepository.Update(id, room);
            if (room == null)
            {
                return NotFound();
            }
            room.UpdatedMetadata(userName);
            await _unitOfWork.SaveAsync();
            var roomDTO = _mapper.Map<RoomDTO>(room);
            return Ok(roomDTO);
        }

        [HttpDelete]
        [Route("MetaDelete/{id:Guid}")]
        public async Task<IActionResult> MetaDelete([FromRoute] Guid id)
        {
            var room = await _unitOfWork.RoomRepository.MetaDelete(id);
            if (room == null)
            {
                return NotFound();
            }
            room.DeletedMetadata(userName);
            await _unitOfWork.SaveAsync();
            var roomDTO = _mapper.Map<RoomDTO>(room);
            return Ok(roomDTO);
        }

    }
}
