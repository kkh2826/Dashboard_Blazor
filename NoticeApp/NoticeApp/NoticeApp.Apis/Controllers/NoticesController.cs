using Microsoft.AspNetCore.Mvc;
using NoticeApp.Models;

namespace NoticeApp.Apis.Controllers
{
    [Route("api/Notices")]
    public class NoticesController : ControllerBase
    {
        private readonly INoticeRepositoryAsync _noticeRepository;
        private readonly ILogger _logger;

        // loggerFactory는 없어도됨
        public NoticesController(INoticeRepositoryAsync noticeRepository, ILoggerFactory loggerFactory)
        {
            this._noticeRepository = noticeRepository;
            this._logger = loggerFactory.CreateLogger(nameof(NoticesController));
        }

        //입력
        // POST api/Notices
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] Notice model)
        {
            model.Created = DateTime.Now;

            // model.Id = 0
            var tmpModel = new Notice();
            tmpModel.Name = model.Name;
            tmpModel.Title = model.Title;
            tmpModel.Content = model.Content;
            tmpModel.ParentId = model.ParentId;
            tmpModel.Created = DateTime.Now;

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var newModel = await _noticeRepository.AddAsync(tmpModel);
                if (newModel == null)
                {
                    return BadRequest();
                }
                //return Ok(newModel); // 200 OK
                var uri = Url.Link("GetNoticeById", new { id = newModel.Id });
                return Created(uri, newModel); // 201 Created
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest();
            }
        }

        // 상세
        // GET api/Notices/1
        [HttpGet("{id}", Name = "GetNoticeById")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                var model = await _noticeRepository.GetByIdAsync(id);
                return Ok(model);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest();
            }
        }


        //출력
        // GET api/Notices
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var notices = await _noticeRepository.GetAllAsync();
                return Ok(notices);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest();
            }
        }

        // 수정
        // PUT api/Notices/1
        [HttpPut("{id}")]
        public async Task<IActionResult> EditAsync(int id, [FromBody] Notice model)
        {
            model.Id = id;

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var status = await _noticeRepository.EditAsync(model);
                if (!status)
                {
                    return BadRequest();
                }
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest();
            }
        }

        // 삭제
        // DELETE api/Notices/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var status = await _noticeRepository.DeleteAsync(id);
                if (!status)
                {
                    return BadRequest();
                }
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest();
            }
        }
    }
}
