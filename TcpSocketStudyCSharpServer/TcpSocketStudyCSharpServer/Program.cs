using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//namespace TcpSocketStudyCSharpServer
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//        }
////    }  https://www.cnblogs.com/johnblogs/p/7087091.html 
//}
//Socket接口原理及用C#语言实现
//首先从原理上解释一下采用Socket接口的网络通讯，这里以最常用的C/S模式作为范例，
//首先，服务端有一个进程（或多个进程）在指定的端口等待客户来连接，服务程序等待客户的连接信息，
//一旦连接上之后，就可以按设计的数据交换方法和格式进行数据传输。客户端在需要的时刻发出向服务端的连接请求。
//这里为了便于理解，提到了一些调用及其大致的功能。使用socket调用后，仅产生了一个可以使用的socket描述符，
//这时还不能进行通信，还要使用其他的调用，以使得socket所指的结构中使用的信息被填写完。

//  在使用TCP协议时，一般服务端进程先使用socket调用得到一个描述符，然后使用bind调用将一个名字与socket描述符连接起来，
//对于Internet域就是将Internet地址联编到socket。之后，服务端使用listen调用指出等待服务请求队列的长度。
//然后就可以使用accept调用等待客户端发起连接，一般是阻塞等待连接，一旦有客户端发出连接，
//accept返回客户的地址信息，并返回一个新的socket描述符，该描述符与原先的socket有相同的特性，
//这时服务端就可以使用这个新的socket进行读写操作了。一般服务端可能在accept返回后创建一个新的进程进行与客户的通信，
//父进程则再到accept调用处等待另一个连接。客户端进程一般先使用socket调用得到一个socket描述符，
//然后使用connect向指定的服务器上的指定端口发起连接，一旦连接成功返回，就说明已经建立了与服务器的连接，
//这时就可以通过socket描述符进行读写操作了。

//server端

using System;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Project1
{
    class Class2
    {
        static void Main()
        {
            try
            {
                int port = 2000;
                string host = "127.0.0.1";
                IPAddress ip = IPAddress.Parse(host);
                IPEndPoint ipe = new IPEndPoint(ip, port);
                Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//创建一个Socket类
                s.Bind(ipe);//绑定2000端口
                s.Listen(0);//开始监听
                Console.WriteLine("Wait for connect");
                Socket temp = s.Accept();//为新建连接创建新的Socket。
                Console.WriteLine("Get a connect");
                string recvStr = "";
                byte[] recvBytes = new byte[1024];
                int bytes;
                bytes = temp.Receive(recvBytes, recvBytes.Length, 0);//从客户端接受信息
                recvStr += Encoding.ASCII.GetString(recvBytes, 0, bytes);
                Console.WriteLine("Server Get Message:{0}", recvStr);//把客户端传来的信息显示出来
                string sendStr = "Ok!Client Send Message Sucessful!";
                byte[] bs = Encoding.ASCII.GetBytes(sendStr);
                temp.Send(bs, bs.Length, 0);//返回客户端成功信息
                temp.Close();
                s.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            Console.WriteLine("Press Enter to Exit");
            Console.ReadLine();
        }
    }
}


