using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FRVN.Data.DataAccess;
using FRVN.Frameworks.Validation;
using BarcodeLib;
using System.Drawing;
using System.IO;
using System.Net;
using ThoughtWorks.QRCode.Codec;

namespace FRVN.Business.Components
{
    public class Code
    {

        public static string CreateVerificationCode(int codeCount)
        {
            string allChar = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            string[] allCharArray = allChar.Split(',');
            string randomCode = "";
            int temp = -1;

            Random rand = new Random();
            for (int i = 0; i < codeCount; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
                }

                int t = rand.Next(36);

                if (temp != -1 && temp == t)
                {
                    return CreateVerificationCode(codeCount);
                }

                temp = t;

                randomCode += allCharArray[t];
            }

            return randomCode;
        }

        public static string CreateAlternateVerificationCode(int codeCount, int alt)
        {
            string allChar = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            string[] allCharArray = allChar.Split(',');
            string randomCode = "";
            int temp = -1;

            Random rand = new Random();
            for (int i = 0; i < codeCount; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * ((int)DateTime.Now.Ticks) * alt);
                }

                int t = rand.Next(36);

                if (temp != -1 && temp == t)
                {
                    return CreateAlternateVerificationCode(codeCount, alt);
                }

                temp = t;

                randomCode += allCharArray[t];
            }

            return randomCode;
        }

        public static Image CreateBarCode(string codeText)
        {
            Barcode barcode = new Barcode()
            {
                IncludeLabel = false, //Can Be True to View
                Alignment = AlignmentPositions.CENTER,
                Width = 250,
                Height = 100,
                RotateFlipType = RotateFlipType.RotateNoneFlipNone,
                BackColor = Color.Transparent,
                ForeColor = Color.Black,
            };

            Image image = barcode.Encode(TYPE.CODE128B, codeText);

            return image;
        }

        public static Image CreateQrCode(string codeText, int scale, int version)
        {

            QRCodeEncoder qrcode = new QRCodeEncoder();

            qrcode.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC;
            qrcode.QRCodeScale = scale;
            qrcode.QRCodeVersion = version;
            qrcode.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            Image image = qrcode.Encode("ALLAN ADITYA PRAKOSA");

            return image;
        }

        public static Image CreateQrCode(string codeText, int length)
        {
            var url = string.Format("http://chart.apis.google.com/chart?cht=qr&chs={1}x{2}&chl={0}", codeText, length, length);
            WebResponse response = default(WebResponse);
            Stream remoteStream = default(Stream);
            StreamReader readStream = default(StreamReader);
            WebRequest request = WebRequest.Create(url);
            response = request.GetResponse();
            remoteStream = response.GetResponseStream();
            readStream = new StreamReader(remoteStream);
            Image image = Image.FromStream(remoteStream);

            response.Close();
            remoteStream.Close();
            readStream.Close();

            return image;
        }
    }
}