//
using System;
using System.Collections;

// This is a duplication of the WatchDog enums
// I don't want util dll's loaded by the WatchDog and possible
// version conflicts, therefor the dupe code

namespace org.auroracoin.aurcore.providers
{
    public class CxPhoneProvider
    {
        private Hashtable mhsPhoneList = new Hashtable();
        
        //
        public CxPhoneProvider() 
        {
            //  http://en.wikipedia.org/wiki/SMS_gateway
            mhsPhoneList.Add(ExPhoneProvider.att, "@txt.att.net");
            mhsPhoneList.Add(ExPhoneProvider.tmobile, "@tmomail.net");
            mhsPhoneList.Add(ExPhoneProvider.verizon, "@vtext.com");
            mhsPhoneList.Add(ExPhoneProvider.verizon_mms, "@vzwpix.com");
            mhsPhoneList.Add(ExPhoneProvider.sprint, "@messaging.sprintpcs.com");
            mhsPhoneList.Add(ExPhoneProvider.alltel, "@message.alltel.com");
            mhsPhoneList.Add(ExPhoneProvider.uscellular, "@email.uscc.net");
            mhsPhoneList.Add(ExPhoneProvider.nextel, "@messaging.nextel.com");
            mhsPhoneList.Add(ExPhoneProvider.cingular, "@cingularme.com");
            mhsPhoneList.Add(ExPhoneProvider.rogers, "@pcs.rogers.com");
            mhsPhoneList.Add(ExPhoneProvider.virgin, "@vmobl.com");
            mhsPhoneList.Add(ExPhoneProvider.ntelos, "@pcs.ntelos.net");
            mhsPhoneList.Add(ExPhoneProvider.metropcs, "@mymetropcs.com");
            //  AT&T (formerly AT&T, then Cingular, now AT&T Wireless - Original grandfathered rateplan customers) 	
            mhsPhoneList.Add(ExPhoneProvider.attmmode, "@mmode.com");
            mhsPhoneList.Add(ExPhoneProvider.centennial, "@cwemail.com");
        }
        
        // Resolve provider enum into email domain
        public string getProviderEmail(ExPhoneProvider pxProvider)
        {
            return (string)mhsPhoneList[pxProvider];
        }

        // Resolve provider enum into email domain
        public string getProviderEmail(string psProviderEnumInt )
        {
            // get value user provided
            int provInt = Int32.Parse(psProviderEnumInt);
            // perform explicit cast 
            ExPhoneProvider exProvider = (ExPhoneProvider)provInt;
            //
            return getProviderEmail( exProvider );
        }

        // Resolve provider int  
        public ExPhoneProvider getProvider(int piProviderInt)
        {
            // perform explicit cast 
            return (ExPhoneProvider)piProviderInt;
        }

        // Resolve string into provider enum 
        static public ExPhoneProvider getProviderEnum(string psProvider)
        {
            return (ExPhoneProvider)Enum.Parse(typeof(ExPhoneProvider), psProvider.ToLower() );
        }
                

    }  // EOC
}