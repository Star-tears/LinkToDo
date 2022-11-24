using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LinkToDo.Myscripts
{
    public class UserInfo
    {
        private readonly string IMG_PATH_PREFIX = "https://src.star-tears.cn/hznu/class-img-bed/";
        public string UUID;
        public string Name { get; set; }
        public string PhoneNum { get; set; }
        public string Email { get; set; }
        public string ImgPath { get; set; }

        public UserInfo()
        {
        }
        public UserInfo(string uUID, string name="", string phoneNum="", string email="", string imgPath="")
        {
            UUID = uUID;
            Name = name;
            PhoneNum = phoneNum;
            Email = email;
            ImgPath = imgPath;
        }
        public string getCompeleteImgPath()
        {
            return IMG_PATH_PREFIX + ImgPath;
        }
        public BitmapImage getImg()
        {
            return new BitmapImage(new Uri(getCompeleteImgPath()));
        }
        public static string genUUID()
        {
            Guid myUUId = Guid.NewGuid();
            string convertedUUID = myUUId.ToString();
            Console.WriteLine("Current UUID is: " + convertedUUID);
            return convertedUUID;
        }
        public static BitmapImage LoadImage(string fileName)
        {
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            int byteLength = (int)fileStream.Length;
            byte[] fileBytes = new byte[byteLength];
            fileStream.Read(fileBytes, 0, byteLength);
            fileStream.Close();

            return ByteArrayToBitmapImage(fileBytes);
        }

        // byte[] --> BitmapImage
        public static BitmapImage ByteArrayToBitmapImage(byte[] array)
        {
            using (var ms = new System.IO.MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad; // here
                image.StreamSource = ms;
                image.EndInit();
                image.Freeze();
                return image;
            }
        }
    }
}
