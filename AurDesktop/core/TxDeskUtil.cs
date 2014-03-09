//
#if DEBUG
//
using System;
using System.Security;
using System.Security.Cryptography;
using Microsoft.Win32;
using System.Text;
//
using NUnit.Framework;



namespace org.auroracoin.desktop.core
{

    [TestFixture]
    public class TxDeskUtil
    {

        [Test]
        public void testReverse()
        {
            string sample = "<td align=\"left\" valign=\"top\"><a href=\"\">GTZY</a></td>";
            string tester = CxDeskUtil.getValueReverse(sample, ">", "</a>");
            Assert.AreEqual( "GTZY", tester);

            sample = "<td align=\"left\" valign=\"top\"><a href=\"\">Wayne Gretzky</a> </td>";
            tester = CxDeskUtil.getValueReverse(sample, ">", "</a>");
            Assert.AreEqual("Wayne Gretzky", tester);
        }


        [Test]
        public void testParseWithReverse()
        {
              string sRet = @"Date of Issue (ET)</h2></td>
                <td align='left' valign='top' width='60'><h2>Range</h2></td>
              </tr>
              <tr>
              4  <td align='left' valign='top'><a href=''>GTZY</a></td>
                <td align='left' valign='top'><a href=''>Wayne Gretzky</a> </td>
                <td align='left' valign='top'>January.23rd.2009</td>
                <td align='left' valign='top'>1,300 - 1,800</td>
              </tr>
              <tr>
              10  <td align='left' valign='top'><a href=''>GTZY</a></td>
                <td align='left' valign='top'><a href='http://www.x.com/wayne_gretzky'>Wayne Gretzky</a> </td>
                <td align='left' valign='top'>January.23rd.2009</td>
                <td align='left' valign='top'>1,300 - 1,800</td>
              </tr>              
              <tr>
                <td colspan='4'>
                    <strong><a href='http://www.x.com/trade/ipoList/'>";

            string[] lines = sRet.Split('\n');
            Console.WriteLine("lines=" + lines.Length);

            string tester = CxDeskUtil.getValueReverse(lines[4], ">", "</a>");
            Assert.AreEqual( "GTZY", tester);

            tester = CxDeskUtil.getValueReverse(lines[5], ">", "</a>");
            Assert.AreEqual("Wayne Gretzky", tester);
        }




    }  // EOC

}
#endif
