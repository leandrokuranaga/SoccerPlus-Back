using ClosedXML.Excel;
using ClosedXML.Extensions;
using Microsoft.AspNetCore.Mvc;
using SoccerPlus.Application.Common;
using SoccerPlus.Domain.SeedWork.Notification;
using SoccerPlus.Infra.Http;
using System.Net;
using static SoccerPlus.Domain.SeedWork.Notification.NotificationModel;

namespace SoccerPlus.Api
{
    public class BaseController : ControllerBase
    {
        private readonly INotification _notification;

        protected BaseController(INotification notification)
        {
            _notification = notification;
        }

        private bool IsValidOperation() => !_notification.HasNotification;

        protected new IActionResult Response(BaseResponse response)
        {
            IActionResult result = null;

            if (IsValidOperation())
            {
                if (response == null)
                    return NoContent();

                result = Ok(response);
            }

            if (!IsValidOperation())
            {
                if (response == null)
                    response = new Response();

                response.Success = false;
                response.Error = _notification.NotificationModel;

                result = VerifyError(_notification.NotificationModel.NotificationType, response);
            }
            return result;
        }

        private IActionResult VerifyError(ENotificationType notificationType, BaseResponse response)
        {
            switch (notificationType)
                {
                    case ENotificationType.BusinessRules:
                        return Conflict(response);
                    case ENotificationType.NotFound:
                        return NotFound(response);
                    case ENotificationType.BadRequestError:
                        return BadRequest(response);
                    default:
                        return StatusCode((int)HttpStatusCode.InternalServerError, response);
                }
        }

        protected new IActionResult Response(object response)
        {
            if (IsValidOperation())
            {
                if (response == null)
                    return NoContent();

                return Ok(new
                {
                    success = true,
                    data = response
                });
            }

            return BadRequest(new
            {
                success = false,
                error = _notification.NotificationModel
            });
        }

        protected new IActionResult Response(int? id, object response)
        {
            if (IsValidOperation())
            {
                if (id == null)
                    return Ok(new
                    {
                        success = true,
                        data = response
                    });

                // ReSharper disable once Mvc.ActionNotResolved
                return CreatedAtAction("Get", new { id },
                    new
                    {
                        success = true,
                        data = response ?? new object()
                    });
            }

            return BadRequest(new
            {
                success = false,
                error = _notification.NotificationModel
            });
        }

        protected IActionResult GetFile(IStorageFile storageFile)
        {
            var memory = new MemoryStream();
            storageFile.Content.CopyTo(memory);

            memory.Position = 0;
            var file = File(memory, storageFile.ContentType, storageFile.Name);
            file.FileDownloadName = storageFile.Name;
            return file;
        }

        protected new IActionResult Response(IXLWorkbook? response, string sheetName)
        {
            if (IsValidOperation())
            {
                if (response is null)
                    return NoContent();

                return ((XLWorkbook)response).Deliver(sheetName);
            }

            return BadRequest(new
            {
                success = false,
                error = _notification.NotificationModel
            });
        }
    }
}
