   M S B A M L   `   `   `  ����@  <Blide, Version=2.1.1.0, Culture=neutral, PublicKeyToken=nullclr-namespace:BlideBlide  5          Blide.Settings   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      %localclr-namespace:Blide     5      .��5
      �   � ColumnDefinitions  5   
   �� 5      $	��99* �6   �� 5      $
��886* �6   .+�5   
   �� -   5        $��SettingsbuttonExit��5   �   $y�Visible=�6   $
��exit��6&   $
��Left=�65   $��793.333,571,0,0q�6P   $	��Top=�6i   $��84��6�   $��32��6�   $��1��6�   �� 5   
   -   5        $��SettingsbuttonApply��5   �   $y�Visible=�6   $��apply��6&   $
��Left=�66   $��703.333,571,0,0q�6Q   $	��Top=�6j   $��84��6�   $��32��6�   $��1��6�   �� 5   
   -   5        $��SettingsbuttonOkay��5   �   $y�Visible=�6   $
��okay��6&   $
��Left=�65   $��613.333,571,0,0q�6P   $	��Top=�6i   $��84��6�   $��32��6�   $��1��6�   �� 5   
   -   5        $��get oauth token here��5      $
��Left=�6/   $��477.333,575,0,0q�6J   $	��Top=�6c   $��28��6{   $	��132��6�   $��13(�6�   $��SemiBold"�6�   
�������$��1��6�   �� 5   
   $
��Left=�6   $
��Wrap=�6/   $	��Top=�6C   $	��557��6[   $	��626��6h   
�������$��22(�6�   $��
ExtraBlack"�6�    �� LineStackingStrategy$ BlockLineHeight=�6�    �� 
LineHeight$ 50��6�   $��2��6�   $��7,0,0,0q�6�   �� 6
  $�	Username:��6  �� 6!  �� 6-  $�
OAuth key:��61  �� 6E  �� 6Q  $�Channel:��6U  �� 6g  �� 6s  �� 6y  �� 6�  �� 6�  �� 6�  �� 6�  �� 6�  $�Command prefix:��6�  �� 6�  �� 6�  $�Delay in ms:��6�  �� 6�  �� 6�  $�Send joinmessage:��6�  6  �� 5   
   $
��Left=�6   $
��Wrap=�6/   $	��Top=�6C   $	��557��6[   $	��626��6h   
�������$��12(�6�   $��SemiBold"�6�   $ BlockLineHeight=�6�   $ 50��6�   $��2��6�   $��6,16,0,0q�6�   �� 6  $�your twitch username��6  �� 6/  �� 6;  $'�!authentication key used by twitch��6?  �� 6j  �� 6v  $�the streamers channel��6z  �� 6�  �� 6�  �� 6�  �� 6�  �� 6�  �� 6�  �� 6�  �� 6�  $�e.g.: /ban, /unban  ��6�  �� 6�  �� 6	  $ �Delay between each command��6  �� 61  �� 6=  $0�*tool will send a message in chat when used��6A  6v  �� 5   
   -   5        $��SettingsTwitchName��5      $��1��6,   $
��Left=�6<   $��29��6W   $��
193,20,0,0q�6c    �� TextWrapping$
 Wrap=�6w   $��
TwitchName��6�   $	��Top=�6�   $	��339��6�   
����������#1b1c1f$��15(�6�   �� 5   
   -   5        $��SettingsTwitchOAuth��5      $��1��6-   $
��Left=�6=   $��29��6X   $��
193,70,0,0q�6d   $
 Wrap=�6x   $*��$oauth:kagewh1234xxxxxxxxxxxxxxxxxxxx��6�   $	��Top=�6�   $	��339��6�   
����������#1b1c1f$��15(�6  �� 5   
   -   5        $��SettingsTwitchChannel��5      $��1��6/   $
��Left=�6?   $��29��6Z   $��193,120,0,0q�6f   $
 Wrap=�6{   $��Channel name��6�   $	��Top=�6�   $	��339��6�   
����������#1b1c1f$��15(�6�   �� 5   
   -   5        $��SettingsTwitchPrefix��5      $��1��6.   $
��Left=�6>   $��29��6Y   $��193,319,0,0q�6e   $
 Wrap=�6z   $��/ban ��6�   $	��Top=�6�   $	��339��6�   
����������#1b1c1f$��15(�6�   �� 5   
   -	   5        $��SettingsTwitchDelay��5      $��1��6-   $
��Left=�6=   $��29��6X   $��193,369,0,0q�6d   $
 Wrap=�6y   $	��300��6�   $	��Top=�6�   $	��339��6�   
����������#1b1c1f$��15(�6�   �� 5   
   $��V 2.0.1��6   $
��Left=�6"   
�������$��22(�6P   $��
ExtraBlack"�6^   $��	635,0,0,0q�6v   $	��Top=�6�   $��65��6�   $	��251��6�   $��1��6�   �� 5   
   $����Open source libraries used:
Twitchlib
Newtonsoft.Json
WebSocketSharp-NonPreRelease
libgit2sharp
Squirrel.windows

special thanks to:
jcmt.dev

��6   $
��Left=�6�   
�������$��12(�6  $��SemiBold"�6&  $��
635,28,0,0q�6<  $	��Top=�6P  $	��160��6h  $	��216��6u  $��1��6�     Blide.ToggleButton  5    
   -
   5          Name$ Bu��5          Blide.ToggleButton   MouseLeftButtonDown$ Bu_MouseLeftButtonDown��6�   $��Center=�6)   $��31��6F   $��146,417,600,167q�6R   $��Center=�6k   $	��140��6�   $��1��6�   5#      5%      