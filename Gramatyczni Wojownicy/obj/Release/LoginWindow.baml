   M S B A M L   `   `   `  ���� P  LGramatyczni Wojownicy, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Gramatyczni_WojownicyGramatyczni_Wojownicy  '    !Gramatyczni_Wojownicy.LoginWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5local#clr-namespace:Gramatyczni_Wojownicy     -     �Name$  loginWindow�� � Title$ LoginWindow��$	��400��$	��300�� � 
ResizeMode$ NoResize=� � ShowInTaskbar .   � WindowStartupLocation$ CenterScreen=� � WindowStyle$
 None=�����  �� ImageSource$# Resources/loginBackground.png��.��� $	��300��$	��400��$��Center=�$��Center=�.+��� -    ��Name$ usersListBox��   PressStart2P#��[  $��90��$	��180��$��Stretch=�$	��Top=�$	��Top=�$��
60,19,60,0q�$��18(�$
��Bold"�$��Disabled=�$��Disabled=�
���������Z� �� -    ��Name$ newUserTextBox��$��Center=�	 �� TextWrapping$
	 Wrap=�$��Tu wpisz login��$��Bottom=�#��[  $��
61,0,61,69q�$��23��$��Center=�$��Center=�$y�Hidden=���Z� ��Z� �� $	��lub��$��Center=�$	��Top=�$��26��$��51��$��Center=�#��[  $��	0,160,0,0q��� -   
 ��Name$
 logInButton��$��Zaloguj sie��$��Center=�$	��Top=�$��	0,125,0,0q�$��20(�#��[  
�������
���   � J� OpacityMask
 �   � J� Opacity$	 0.4Q��� -   $
 newUserButton��$��Załóż konto��$��Center=�$	��Top=�$��35,186,35,0q�$��20(�#��[  
�������
���   �
 �   ��� -   $
 createUserButton��$��Stwórz��$��Center=�$	��Top=�$��35,336,35,0q�$��20(�#��[  
�������
���   �
 �   �$y�Hidden=�