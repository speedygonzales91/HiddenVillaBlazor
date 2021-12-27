using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiddenVilla_Client.Helper
{
    public static class IJSRuntimeExtension
    {
        public static async ValueTask ToastrSuccess(this IJSRuntime jSRuntime, string message)
        {
            await jSRuntime.InvokeVoidAsync("ShowToastr", "succes", message);
        }

        public static async ValueTask ToastrError(this IJSRuntime jSRuntime, string message)
        {
            await jSRuntime.InvokeVoidAsync("ShowToastr", "error", message);
        }

        public static async ValueTask SwalSuccess(this IJSRuntime jSRuntime, string message, string submessage)
        {
            await jSRuntime.InvokeVoidAsync("ShowSwal", "succes", message, submessage);
        }

        public static async ValueTask SwalError(this IJSRuntime jSRuntime, string message, string submessage)
        {
            await jSRuntime.InvokeVoidAsync("ShowSwal", "error", message, submessage);
        }

    }
}
