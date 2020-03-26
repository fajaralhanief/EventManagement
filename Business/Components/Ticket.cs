using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using TheArtOfDev.HtmlRenderer.Core;
using TheArtOfDev.HtmlRenderer.Adapters;
using TheArtOfDev.HtmlRenderer.WinForms;
using System.Configuration;
using FRLN.Business.Component;

namespace FRVN.Business.Components
{
    public class Ticket
    {
        public static void CreateTicket(string scriptsHTML, string fileName, string code)
        {
            PointF point = new PointF(0, 0);
            PointF _subPpoint = new PointF(600, 55);

            Image _image = Image.FromFile((ConfigurationManager.AppSettings["TicketTemplateURL"]));
            _image = _image.GetThumbnailImage(755, 415, null, IntPtr.Zero);

            Image _barcode = Code.CreateBarCode(code);
            _barcode.RotateFlip(RotateFlipType.Rotate270FlipX);

            HtmlRender.RenderToImage(_image, scriptsHTML, point);
            HtmlRender.RenderToImage(_barcode, "", _subPpoint);
            _image.Save(@"" + (ConfigurationManager.AppSettings["TicketURL"] + fileName  + ".png"), ImageFormat.Png);
        }
    }
}