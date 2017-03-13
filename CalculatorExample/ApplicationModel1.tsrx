<?xml version="1.0" encoding="utf-8"?>
<applicationModel>
  <name>Application Model</name>
  <className>CalcAppModel</className>
  <objects>
    <object technology="Standard Windows" type="Window">
      <name>Calculator</name>
      <codeName>CalculatorWindow</codeName>
      <identification>
        <properties>
          <property name="IsOwnedWindow" type="boolean">false</property>
          <property name="IsChildWindow" type="boolean">false</property>
          <property name="WindowClassRegExp" type="string">CalcFrame</property>
          <property name="WindowTitleRegExp" type="string">Calculator</property>
        </properties>
      </identification>
      <children>
        <object technology="Standard Windows" type="Button">
          <name>=</name>
          <codeName>MyEqualsButton</codeName>
          <identification>
            <properties>
              <property name="Text" type="string">=</property>
              <property name="NativeClass" type="string">Button</property>
            </properties>
          </identification>
        </object>
      </children>
    </object>
  </objects>
</applicationModel>