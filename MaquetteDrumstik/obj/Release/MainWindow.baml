   M S B A M L   `   `   `  ����   MaquetteDrumstik!    MaquetteDrumstik.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     7svgc&http://sharpvectors.codeplex.com/svgc/        PrimaryBackgroundWin#��[    � Title$  
MainWindow��$	��700��$
��1172�� � DataContext ��  �� RelativeSource �� *Self+.���  J� RenderTransformOrigin$ 0.502,0.542&� � RowDefinitions �� $��50 ��� $��* � � ColumnDefinitions �� $��0 ��� .+�� $��1��$��2��$��
0,0,1164,0q��� -    ��Name$ scroll��$
��Auto=�$��Visible=�$��1��$��1��$��
0,50,0,199q�$	��Top=�.����  �� Orientation$ 
Horizontal=�.+��� $
��20 0q�$ 
Horizontal=�.+��� -    ��Name$ unexo��  Primary#��[ ���� 	 �� Path$	 currentExercices����� .R��� .+��� $	��110��$	��140��
 �� CornerRadius$
 5��$��5q� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     ShadowDepth$ 1Q�����  �� ImageSource �� *ThumbnailLocalPath+��  ��Name$ poggers��  Txt#��[ $��5q�$��27(�$��Franklin Gothic Medium)����� *title+ �� StringFormat$ {0:C}�� �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=��� $	��5 0q�$��15(�#��[ �� $�Niveau de l'exercice : ���� $� ���� ��� *level+���� .R�� �� -   $ ListViewProducts��#��[ ���� $	 	exercices����� .R��� .+��� $	��100��$	��100��$
 5��$��5q�   $ 1Q�����  �� *ThumbnailLocalPath+�� #��[ $��5q�$��17(�$��Franklin Gothic Medium)����� *title+�� $	��5 0q�$��15(�#��[ �� $�Niveau de l'exercice : ���� $� ���� ��� *level+���� .R�� �� -    ��Name$ 	lesimages������ .R��� #��[ $��17(����� *path+���� .R�� �� -   $��	Recherche��  TextBoxBaseControlTemplate#��[ $��1��$��23��$��
0,13,114,0q� �� TextWrapping$
 Wrap=�$�� ��$	��Top=�$	��120��
 � Tag$ Rechercher un Exercice...��$��Right=�c� -    c�Name$ 	LaToolbar��  	Secondary#��[ $��1��$
��Left=�$��50��$	��Top=�$	��650��$��	10,-5,0,0q�.@��� -   $��btnacc��
������ .���� $��Asset/D.png��$��Right=�$��30��$��32���� -   $
��btn1��
������ .����  �� Padding$ 5q�$��18(�$��Center=�$��Center=�$	��170��$��Online Content��#��[ $��0,10,0,9q�$��40���� -	   $
��btn2��
������ .���� $ 5q�$��18(�$��Center=�$��Center=�$	��129��$��My Files��#��[ $��0,10,0,9q�$��39���� -
   $
��btn3��
������ .���� $ 5q�$��18(�$��Center=�$��Center=�$	��116��$��Virtual Kit��#��[ $��0,10,0,9q�$��38���� 
������ $��1��$��Right=�$��	0,13,81,0q�$	��Top=�$��28��$��27��$ 3.339,0.907&�.���� $��Asset/Unecloche.png��$��Right=��� -   $��SliderGauche��
������ $��2��$��Right=�$��
0,21,108,0q�$��1��$	��Top=�$��37��$��24��.���� $��Asset/chleft-w.png��$��Right=��� -   $��sliderDroit��
������ $��2��$��Right=�$��	0,21,66,0q�$��1��$	��Top=�$��37��$��24��.���� $��Asset/chright-w.png���� -   $��2��$��RefreshToken��$
��Left=�$��1079,589,0,0q�$��1��$��Bottom=�$��75���� -   $��2��$��Modifier��$
��Left=�$��
10,580,0,0q�$��1��$��Bottom=�$	��110��$��29���� $��2��$
��Left=�$��
10,249,0,0q�$��1��#��[ $
��Wrap=�$��My Files��$	��Top=�$��31��$	��110��$��20(� �� TextAlignment$ Center=��� -   $ MyfilesObservable��$��Disabled=�$
��Auto=�$��1��$��1��$��	0,300,0,0q�$	��Top=�.���� $ 
Horizontal=�.+��� $
��20 0q�$ 
Horizontal=�.+��� -   $
 open��#��[ ���� $	 
listefiles����� .R��� .+��� $	��230��$	��350��$
 5��$��5q�   $ 1Q�����  �� *url+�� #��[ $��5q�$��17(�$��Franklin Gothic Medium)����� *title+���� .R�� � 