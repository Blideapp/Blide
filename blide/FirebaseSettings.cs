using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Auth;


namespace Blide
{
    class FirebaseSettings
    {
        /*
        IFirebaseConfig firebaseconfig = new FirebaseConfig
        {
            AuthSecret = "",
            BasePath = ""
        };
        */
        //FirestoreDb db = FirestoreDb.Create(project);

        

        public FirebaseSettings()
        {
            
        }

        public Boolean isLoggedIn()
        {
            return false;
        }

        public void login(string email, string password)
        {
            throw new NotImplementedException();
        }
        public void signup(string name, string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
