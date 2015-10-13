using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SKYPE4COMLib;

namespace SkypeBot2
{
    [Serializable]
    public class Conf
    {

        //  delegate void UI();
        //event UI msgProcessed;
        //public Conf(string _skypeName):this()
        //{
        //    skypeName = _skypeName;
        //}
        //public Conf()
        //{
        //    messages = new Queue<ChatMessage>();
        //    msgProcessed += Сonference_msgProcessed;
        //}

        //void Сonference_msgProcessed()
        //{
        //    processNext();
        //}

        //string name=string.Empty;
        //string skypeName;
        //public string SkypeName { 
        //    get { return skypeName; }
        //    set { skypeName = value; }
        //}
        //public string Name
        //{
        //    get { return name; }
        //    set { value = name; }
        //}
        //Queue<ChatMessage> messages;
        //public void AddMessage(ChatMessage msg)
        //{
        //    messages.Enqueue(msg);
        //    if (messages.Count == 1)
        //    {
        //        //SeijaHelper.SendToMaster("Начинаю обработку");
        //        processNext();
        //    }
        //}
        
        //public void processNext()
        //{
        //    if (messages.Count > 0)
        //    {
        //       // SeijaHelper.SendToMaster("Передаю сообщение");
        //        SeijaCommander.Seija.ProcessMSG(messages.Peek());
        //        //SeijaHelper.SendToMaster("Выкидываю из стека");
        //        messages.Dequeue();
        //        processNext();
        //    }

        //        //processMsg(messages.Dequeue());
        //}


    }
}
