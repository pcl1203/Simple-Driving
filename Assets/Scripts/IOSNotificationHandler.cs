using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_IOS
using Unity.Notifications.iOS;
#endif
public class IOSNotificationHandler : MonoBehaviour
{
#if UNITY_IOS
   private const string ChannelId = "notification_channel";
   public void ScheduleNotification(int minutes)
   {
      iOSNotification notification = new iOSNotification
      {
         Title = "Energy Recharged!",
         Subtitle = "Your Energy has been recharged",
         Body = "Laro ka na ulit!",
         ShowInForeground = true,
         ForegroundPresentationOption = (PresentationOption.Alert | PresentationOption.Sound),
         CategoryIdentifier = "category_a",
         ThreadIdentifier = "thread1",
         Trigger = new iOSNotificationTimeIntervalTrigger
         {
            TimeInterval = new System.TimeSpan(0, minutes, 0),
            Repeats = false
         }
      };

     iOSNotificationCenter.ScheduleNotification(notification);
   }
#endif
}
