   M S B A M L   `   `   `  ���� P  LGramatyczni Wojownicy, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Gramatyczni_WojownicyGramatyczni_Wojownicy  &     Gramatyczni_Wojownicy.PlotWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     R NOxyPlot.Wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0 oxyhttp://oxyplot.org/wpf  /local#clr-namespace:Gramatyczni_Wojownicy    � Title$  
PlotWindow��$	��450��$	��525�� � WindowStartupLocation$ CenterScreen=� � WindowStyle$
 None=�����  �� ImageSource$# Resources/plot_background.png�� � DataContext $   Gramatyczni_Wojownicy.PlotData  .���  � RowDefinitions �� $��50 ��� �� $��50 �.+�  OxyPlot.Wpf.Plot    OxyPlot.Wpf.PlotBase   ZoomHorizontalCursor$
 None��   ZoomRectangleCursor$
 None��$��1��   Title �� *Title+���� 	   Series	   OxyPlot.Wpf.LineSeries  ���� *Points+
   Axes
   OxyPlot.Wpf.LinearAxis    OxyPlot.Wpf.Axis   Position$
 Left=�   AbsoluteMinimum$ 0Q�   AbsoluteMaximum$	 100Q��� -    ��Name$ userPlotLabel��$(��"Statystyki użytkownika xxxxxxxxxx��$��Center=�$��0,10,0,0q�
�������   PressStart2P#���   $��14(��� -   $ closePlotButton��$��Zamknij��$��Center=�$��406,5,10,10q�$��2��$��Center=� J� RenderTransformOrigin$ 0.342,-0.526&�
�������#���   $��Center=�$��Center=�$��16(�$	��101��$��35���� $&�� Oś Y - procentowa skuteczność��$
��Left=�$��2��$	��Top=�
�������#���   �� $��Oś X - liczba gier��$
��Left=�$��0,28,0,0q�$��2��$	��Top=�
�������#���   