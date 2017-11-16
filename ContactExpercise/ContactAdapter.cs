using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Database;
using Android.Provider;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace ContactExpercise
{
    public class ContactAdapter : BaseAdapter
    {
        Activity activity;
        List<Contacts> ContactList;

        public ContactAdapter(Activity activity)
        {
            this.activity = activity;

            FillContacts();
        }

        public override int Count {
            get { return ContactList.Count; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return ContactList[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.ContactListItem, parent, false);
            var contactName = view.FindViewById<TextView>(Resource.Id.contactName);
            var contactImage = view.FindViewById<ImageView>(Resource.Id.contactImage);

            contactName.Text = ContactList[position].DisplayName;

            if(ContactList[position].PhotoId == null){
                contactImage.SetImageResource(Resource.Drawable.ContactImage);
            }
            return view;

        }

        void FillContacts()
        {
            var uri = ContactsContract.Contacts.ContentUri;
            string[] projection = {
                ContactsContract.Contacts.InterfaceConsts.Id,
                ContactsContract.Contacts.InterfaceConsts.DisplayName,
                ContactsContract.Contacts.InterfaceConsts.PhotoId
            };

            var loader = new CursorLoader(activity, uri, projection, null, null, ContactsContract.Contacts.InterfaceConsts.DisplayName);
            var cursor = (ICursor)loader.LoadInBackground();

            ContactList = new List<Contacts>();

            if(cursor.MoveToFirst())
            {
                do
                {
                    ContactList.Add(new Contacts
                    {
                        Id = cursor.GetLong(cursor.GetColumnIndex(projection[0])),
                        DisplayName = cursor.GetString(cursor.GetColumnIndex(projection[1])),
                        PhotoId = cursor.GetString(cursor.GetColumnIndex(projection[2]))

                        
                    });
                }
                while (cursor.MoveToNext());
            }

        }
    }
}
