/**
 * @docauthor 译者: 大平<283713763@qq.com>
 * Base class for all Ext components. All subclasses of Component may participate in the automated Ext component
 * lifecycle of creation, rendering and destruction which is provided by the {@link Ext.container.Container Container}
 * class. Components may be added to a Container through the {@link Ext.container.Container#cfg-items items} config option
 * at the time the Container is created, or they may be added dynamically via the
 * {@link Ext.container.Container#method-add add} method.
 * 所有Ext组件的基类,组件下所有的子类都可能参与自动化Ext组件的生命周期执行创建,
 * 渲染和销毁都是由{@link Ext.container.Container 
 * Container}容器类提供,组件可以通过容器配置{@link Ext.container.Container#cfg-items items}创建,
 * 也可以通过动态方法
 * {@link Ext.container.Container#method-add add}创建
 *
 * The Component base class has built-in support for basic hide/show and enable/disable and size control behavior.
 * 组件基类内置了组件的 隐藏/显示,启用/禁用,大小控制行为.
 *
 * All Components are registered with the {@link Ext.ComponentManager} on construction so that they can be referenced at
 * any time via {@link Ext#getCmp Ext.getCmp}, passing the {@link #id}.
 * 所有组件被注册在布局管理器中{@link Ext.ComponentManager},
 * 这样就可以通过{@link Ext#getCmp Ext.getCmp}随时被引用,
 *
 * All user-developed visual widgets that are required to participate in automated lifecycle and size management should
 * subclass Component.
 * 所有用户自定义的可视部件,他们的生命周期和规模
 * 管理的子类组件都必须参加.
 *
 * See the Creating new UI controls chapter in [Component Guide][1] for details on how and to either extend
 * or augment Ext JS base classes to create custom Components.
 * 请参阅创建新的UI控件在[Component Guide][1]如何扩展或增加
 * Ext JS的基类来创建自定义组件的详细信息。
 *
 * Every component has a specific xtype, which is its Ext-specific type name, along with methods for checking the xtype
 * like {@link #getXType} and {@link #isXType}. See the [Component Guide][1] for more information on xtypes and the
 * Component hierarchy.
 * 每种组件都有特定的类型，是Ext自身设置的类型。
 * 对xtype检查的相关方法如getXType和isXType。
 * 
 *
 * This is the list of all valid xtypes:
 * 这里是所有有效的xtypes列表
 *
 *     xtype            Class
 *     -------------    ------------------
 *     button           {@link Ext.button.Button}
 *     buttongroup      {@link Ext.container.ButtonGroup}
 *     colorpalette     {@link Ext.picker.Color}
 *     component        {@link Ext.Component}
 *     container        {@link Ext.container.Container}
 *     cycle            {@link Ext.button.Cycle}
 *     dataview         {@link Ext.view.View}
 *     datepicker       {@link Ext.picker.Date}
 *     editor           {@link Ext.Editor}
 *     editorgrid       {@link Ext.grid.plugin.Editing}
 *     grid             {@link Ext.grid.Panel}
 *     multislider      {@link Ext.slider.Multi}
 *     panel            {@link Ext.panel.Panel}
 *     progressbar      {@link Ext.ProgressBar}
 *     slider           {@link Ext.slider.Single}
 *     splitbutton      {@link Ext.button.Split}
 *     tabpanel         {@link Ext.tab.Panel}
 *     treepanel        {@link Ext.tree.Panel}
 *     viewport         {@link Ext.container.Viewport}
 *     window           {@link Ext.window.Window}
 *
 *     Toolbar components
 *     ---------------------------------------
 *     pagingtoolbar    {@link Ext.toolbar.Paging}
 *     toolbar          {@link Ext.toolbar.Toolbar}
 *     tbfill           {@link Ext.toolbar.Fill}
 *     tbitem           {@link Ext.toolbar.Item}
 *     tbseparator      {@link Ext.toolbar.Separator}
 *     tbspacer         {@link Ext.toolbar.Spacer}
 *     tbtext           {@link Ext.toolbar.TextItem}
 *
 *     Menu components
 *     ---------------------------------------
 *     menu             {@link Ext.menu.Menu}
 *     menucheckitem    {@link Ext.menu.CheckItem}
 *     menuitem         {@link Ext.menu.Item}
 *     menuseparator    {@link Ext.menu.Separator}
 *     menutextitem     {@link Ext.menu.Item}
 *
 *     Form components
 *     ---------------------------------------
 *     form             {@link Ext.form.Panel}
 *     checkbox         {@link Ext.form.field.Checkbox}
 *     combo            {@link Ext.form.field.ComboBox}
 *     datefield        {@link Ext.form.field.Date}
 *     displayfield     {@link Ext.form.field.Display}
 *     field            {@link Ext.form.field.Base}
 *     fieldset         {@link Ext.form.FieldSet}
 *     hidden           {@link Ext.form.field.Hidden}
 *     htmleditor       {@link Ext.form.field.HtmlEditor}
 *     label            {@link Ext.form.Label}
 *     numberfield      {@link Ext.form.field.Number}
 *     radio            {@link Ext.form.field.Radio}
 *     radiogroup       {@link Ext.form.RadioGroup}
 *     textarea         {@link Ext.form.field.TextArea}
 *     textfield        {@link Ext.form.field.Text}
 *     timefield        {@link Ext.form.field.Time}
 *     trigger          {@link Ext.form.field.Trigger}
 *
 *     Chart components
 *     ---------------------------------------
 *     chart            {@link Ext.chart.Chart}
 *     barchart         {@link Ext.chart.series.Bar}
 *     columnchart      {@link Ext.chart.series.Column}
 *     linechart        {@link Ext.chart.series.Line}
 *     piechart         {@link Ext.chart.series.Pie}
 *
 * It should not usually be necessary to instantiate a Component because there are provided subclasses which implement
 * specialized Component use cases which cover most application needs. However it is possible to instantiate a base
 * Component, and it will be renderable, or will particpate in layouts as the child item of a Container:
 * 组件(component)通常不被实例化，因为有子类实现专门的组件.
 * 很多时候覆盖了大部分的应用需求。
 * 但是，它是可以实例化的基础组件，它会被渲染，或将参与一个容器的子项的布局
 *
 *     @example
 *     Ext.create('Ext.Component', {
 *         html: 'Hello world!',
 *         width: 300,
 *         height: 200,
 *         padding: 20,
 *         style: {
 *             color: '#FFFFFF',
 *             backgroundColor:'#000000'
 *         },
 *         renderTo: Ext.getBody()
 *     });
 *
 * The Component above creates its encapsulating `div` upon render, and use the configured HTML as content. More complex
 * internal structure may be created using the {@link #renderTpl} configuration, although to display database-derived
 * mass data, it is recommended that an ExtJS data-backed Component such as a {@link Ext.view.View View}, or {@link
 * Ext.grid.Panel GridPanel}, or {@link Ext.tree.Panel TreePanel} be used.
 * 上面通过组件创建封装并在'div'上渲染.使用配置的html作为内容,可以创建更多复杂的内部结构,
 * 虽然支持renderTo的配置来显示数据,
 * 但建议使用EXTJS 数据支持组件
 * 例如Ext.view.View View 或者 Ext.grid.Panel GridPanel 或者 Ext.tree.Panel TreePanel
 *
 * [2]: #!/guide/components
 */
Ext.define('Ext.Component', {

    /* Begin Definitions */
    /* 开始定义 */

    alias: ['widget.component', 'widget.box'],

    extend: 'Ext.AbstractComponent',

    requires: [
        'Ext.util.DelayedTask'
    ],

    uses: [
        'Ext.Layer',
        'Ext.resizer.Resizer',
        'Ext.util.ComponentDragger'
    ],

    mixins: {
        floating: 'Ext.util.Floating'
    },

    statics: {
        // Collapse/expand directions
        // 折叠/展开 的方向
        DIRECTION_TOP: 'top',
        DIRECTION_RIGHT: 'right',
        DIRECTION_BOTTOM: 'bottom',
        DIRECTION_LEFT: 'left',

        VERTICAL_DIRECTION_Re: /^(?:top|bottom)$/,

        // RegExp whih specifies characters in an xtype which must be translated to '-' when generating auto IDs.
        // This includes dot, comma and whitespace
        // 正则表达式，它指定类型的字符，必须被转换为'-'自动生成ID。
        // 这点，逗号和空格的
        INVALID_ID_CHARS_Re: /[\.,\s]/g
    },

    /* End Definitions */
    /* 结束定义 */

    /**
     * @cfg {Boolean/Object} resizable
     * Specify as `true` to apply a {@link Ext.resizer.Resizer Resizer} to this Component after rendering.
     *
     * May also be specified as a config object to be passed to the constructor of {@link Ext.resizer.Resizer Resizer}
     * to override any defaults. By default the Component passes its minimum and maximum size, and uses
     * `{@link Ext.resizer.Resizer#dynamic}: false`
     */
     /**
     * @cfg {Boolean/Object} resizable
     * 默认指定为'true'来适应这个组件渲染之后{@link Ext.resizer.Resizer Resizer} 
     *
     * 也可以指定一个配置对象被传递到覆盖任何默认的构造函数
     * 默认情况下,组件的最大化,最小化使用 'false'
     * 
     */

    /**
     * @cfg {String} resizeHandles
     * A valid {@link Ext.resizer.Resizer} handles config string. Only applies when resizable = true.
     * 只有在自动大小为true的情况下,配置字符串才有效
     */
    resizeHandles: 'all',

    /**
     * @cfg {Boolean} [autoScroll=false]
     * `true` to use overflow:'auto' on the components layout element and show scroll bars automatically when necessary,
     * `false` to clip any overflowing content.
     * This should not be combined with {@link #overflowX} or  {@link #overflowY}.
     * 'true'使用溢出：'自动'的组件布局元素，并在必要时自动显示滚动条，
     * 'false'溢出的内容。
     * 不能像这样定义{@link #overflowX} or  {@link #overflowY}.
     */

    /**
     * @cfg {String} overflowX
     * Possible values are:
     *  * `'auto'` to enable automatic horizontal scrollbar (overflow-x: 'auto').
     *  * `'scroll'` to always enable horizontal scrollbar (overflow-x: 'scroll').
     * The default is overflow-x: 'hidden'. This should not be combined with {@link #autoScroll}.
     * 可能的值有:
     * * `'auto'` 自动启用水平滚动条(水平溢出:'auto')
     * * `'scroll'` 始终启用水平滚动条(水平溢出:'scroll')
     * 默认的水平溢出是'hidden',不能这样指定{@link #autoScroll}.又自动又始终
     */

    /**
     * @cfg {String} overflowY
     * Possible values are:
     *  * `'auto'` to enable automatic vertical scrollbar (overflow-y: 'auto').
     *  * `'scroll'` to always enable vertical scrollbar (overflow-y: 'scroll').
     * The default is overflow-y: 'hidden'. This should not be combined with {@link #autoScroll}.
     * 可能的值有:
     * * `'auto'` 自动启用自动垂直滚动条(垂直溢出:'auto')
     * * `'scroll'` 始终启用垂直滚动条(垂直溢出:'scroll')
     * 默认的垂直溢出是'hidden',不能这样指定{@link #autoScroll}.又自动又始终
     */

    /**
     * @cfg {Boolean} floating
     * Specify as true to float the Component outside of the document flow using CSS absolute positioning.
     *
     * Components such as {@link Ext.window.Window Window}s and {@link Ext.menu.Menu Menu}s are floating by default.
     *
     * Floating Components that are programatically {@link Ext.Component#render rendered} will register themselves with
     * the global {@link Ext.WindowManager ZIndexManager}
     *
     * ### Floating Components as child items of a Container
     *
     * A floating Component may be used as a child item of a Container. This just allows the floating Component to seek
     * a ZIndexManager by examining the ownerCt chain.
     *
     * When configured as floating, Components acquire, at render time, a {@link Ext.ZIndexManager ZIndexManager} which
     * manages a stack of related floating Components. The ZIndexManager brings a single floating Component to the top
     * of its stack when the Component's {@link #toFront} method is called.
     *
     * The ZIndexManager is found by traversing up the {@link #ownerCt} chain to find an ancestor which itself is
     * floating. This is so that descendant floating Components of floating _Containers_ (Such as a ComboBox dropdown
     * within a Window) can have its zIndex managed relative to any siblings, but always **above** that floating
     * ancestor Container.
     *
     * If no floating ancestor is found, a floating Component registers itself with the default {@link Ext.WindowManager
     * ZIndexManager}.
     *
     * Floating components _do not participate in the Container's layout_. Because of this, they are not rendered until
     * you explicitly {@link #method-show} them.
     *
     * After rendering, the ownerCt reference is deleted, and the {@link #floatParent} property is set to the found
     * floating ancestor Container. If no floating ancestor Container was found the {@link #floatParent} property will
     * not be set.
     */
     /**
     * @cfg {Boolean} floating
     * 指定为'true'的时候浮动组件以外的文档流中使用CSS绝对定位。
     * 
     * Ext.window.Window Window and Ext.menu.Menu Menu 组件默认情况下是浮动的.
     *
     * 浮动的组件编程方式将自己{@link Ext.Component#render rendered}注册到全局{@link Ext.WindowManager ZIndexManager}
     *
     * ### 作为一个容器的子项的浮动组件
     *
     * 一个浮动的组件，也可以使用一个容器作为一个子项。这只是允许的浮动组件，
     * This just allows the floating Component to seek a ZIndexManager by examining the ownerCt chain.
     *
     * 当配置为浮动，组件获取，在渲染时，一个{@link Ext.ZIndexManager ZIndexManager}管理堆栈相关的浮动元件。
     * ZIndexManager带来一个单一的浮动组件组件{@link #toFront}的方法被调用时，它的堆栈的顶部。
     *
     * ZIndexManager通过遍历链找到一个被继承的{@link #ownerCt}这本身就是浮动的。
     * 这使子类浮动容器组件（如在一个窗口一个ComboBox下拉）可以有它的zIndex的管理
     * 相对于任何同级节点，但始终高于浮动被继承的对象。
     *
     * 如果没有父类的浮动定义，浮动组件注册本身的默认
     * {@link Ext.WindowManager ZIndexManager}.
     *
     * 在容器的布局，浮动组件不参与。正因为如此，他们都不会呈现，直到您明确。{@link #method-show} 
     *
     * 渲染后，父类浮动引用将被删除，该属性设置为找到的父类容器{@link #floatParent}。
     * 如果没有浮动的父类容器{@link #floatParent}的属性
     * 不会被删除.
     */
    floating: false,

    /**
     * @cfg {Boolean} toFrontOnShow
     * 如果为True，则自动调用{@link #toFront}，当{@link #method-show}被调用已经可见的，
     * 浮动的组件。浮动组件的层在最
     */
    toFrontOnShow: true,

    /**
     * @property {Ext.ZIndexManager} zIndexManager
     * 只存在于{@link #floating}组件后，他们已提供。
     *
     * 管理该组件的ZIndexManager的参考是z-index。
     *
     * {@link Ext.ZIndexManager ZIndexManager}维护一个堆栈的浮动组件z轴，
     * 并且还提供了一个单一模态面具下是插入模态浮动组件的可见。
     *
     * 浮动组件可能是 {@link #toFront brought to the front} or {@link #toBack sent to the back} of the
     * z轴 堆栈.
     *
     * 这个默认为全局{@link Ext.WindowManager ZIndexManager} 浮动组件
     * 他们以编程方式访问{@link Ext.Component#render rendered}.
     *
     * 对于浮动{@link #floating}组件被添加到一个容器中，
     * ZIndexManager首先回收第一个父类的浮动状态,
     * 如果没有浮动层,全局{@link Ext.WindowManager ZIndexManager}就被使用
     *
     * See {@link #floating} and {@link #zIndexParent}
     * @readonly
     */

    /**
     * @property {Ext.Container} floatParent
     * 只存在于作为容器的子项被插入的浮动组件。
     *
     * 浮动的组件编程方式{@link Ext.Component#render rendered}
     * 将不会有浮动的父级属性
     *
     * 对于{@link #floating}浮动组件是一个容器的子项，浮动上级将是拥有容器
     *
     * 例如，在一个窗口的下拉列表{@link Ext.view.BoundList BoundList} 在窗体中,
     * 它的浮动父窗口就是窗体
     * 
     * See {@link #floating} and {@link #zIndexManager}
     * @readonly
     */

    /**
     * @property {Ext.Container} zIndexParent
     * 只存在于被插入的{@link #floating}浮动组件作为子项的容器，
     * 有一个浮动的容器包含在其中。
     *
     * 对于{@link #floating}浮动组件是一个容器的子项，zIndexParent将是一个浮动的顶层容器，
     * 负责其所有浮动的子类z-index值。
     * 它提供了{@link Ext.ZIndexManager ZIndexManager}的Z-索引服务，
     * 提供其所有的后代浮动组件。
     * For {@link #floating} Components which are child items of a Container, the zIndexParent will be a floating
     * ancestor Container which is responsible for the base z-index value of all its floating descendants. It provides
     * a {@link Ext.ZIndexManager ZIndexManager} which provides z-indexing services for all its descendant floating
     * Components.
     *
     * 浮动的组件{@link Ext.Component#render rendered}编程方式呈现，
     * 不会有一个'zIndexParent`属性
     *
     * 例如，在一个窗口这是一个ComboBox的的下拉{@link Ext.view.BoundList BoundList} 的窗口，
     * 它的zIndexParent，上面显示该窗口，无论被放置在窗口的z-index堆栈。
     * For example, the dropdown {@link Ext.view.BoundList BoundList} of a ComboBox which is in a Window will have the
     * Window as its `zIndexParent`, and will always show above that Window, wherever the Window is placed in the z-index stack.
     *
     * See {@link #floating} and {@link #zIndexManager}
     * @readonly
     */

    /**
     * @cfg {Boolean/Object} [draggable=false]
     * 指定正确的使用组件的封装元件的拖动手柄进行{@link #floating}
     * 浮动组件拖动的。
     * Specify as true to make a {@link #floating} Component draggable using the Component's encapsulating element as
     * the drag handle.
     *
     * 这也可以被指定作为配置对象被实例化的{@link Ext.util.ComponentDragger ComponentDragger}
     * 执行拖动。
     * This may also be specified as a config object for the {@link Ext.util.ComponentDragger ComponentDragger} which is
     * instantiated to perform dragging.
     *
     * 例如，创建一个组件，只能将它拖动周围使用某些内部元件的句柄，
     * 使用委托选项：
     * For example to create a Component which may only be dragged around using a certain internal element as the drag
     * handle, use the delegate option:
     *
     *     new Ext.Component({
     *         constrain: true,
     *         floating: true,
     *         style: {
     *             backgroundColor: '#fff',
     *             border: '1px solid black'
     *         },
     *         html: '<h1 style="cursor:move">The title</h1><p>The content</p>',
     *         draggable: {
     *             delegate: 'h1'
     *         }
     *     }).show();
     */

    hideMode: 'display',
    // Deprecate 5.0
    hideParent: false,

    bubbleEvents: [],

    actionMode: 'el',
    monPropRe: /^(?:scope|delay|buffer|single|stopEvent|preventDefault|stopPropagation|normalized|args|delegate)$/,

    //renderTpl: new Ext.XTemplate(
    //    '<div id="{id}" class="{baseCls} {cls} {cmpCls}<tpl if="typeof ui !== \'undefined\'"> {uiBase}-{ui}</tpl>"<tpl if="typeof style !== \'undefined\'"> style="{style}"</tpl>></div>', {
    //        compiled: true,
    //        disableFormats: true
    //    }
    //),

    /**
     * Creates new Component.
     * @param {Ext.Element/String/Object} config The configuration options may be specified as either:
     *
     * - **an element** : it is set as the internal element and its id used as the component id
     * - **an element** :它被设置为在内部元件和使用它的id作为组分的id
     * - **a string** : it is assumed to be the id of an existing element and is used as the component id
     * - **a string** : 它被假定为是一个现有元素的id和被用作组件id
     * - **anything else** : it is assumed to be a standard config object and is applied to the component
     * - **anything else** : 它被假定为是一个标准的配置对象，并且被施加到该组件
     */
    constructor: function(config) {
        var me = this;

        config = config || {};
        if (config.initialConfig) {

            // Being initialized from an Ext.Action instance...
            // 从Ext.Action实例被初始化
            if (config.isAction) {
                me.baseAction = config;
            }
            config = config.initialConfig;
            // component cloning / action set up
            // 组件克隆/动作设置
        }
        else if (config.tagName || config.dom || Ext.isString(config)) {
            // element object
            config = {
                applyTo: config,
                id: config.id || config
            };
        }

        me.callParent([config]);

        // If we were configured from an instance of Ext.Action, (or configured with a baseAction option),
        // register this Component as one of its items
        // 如果我们从一个实例的Ext.Action，（或配置与baseAction选项）配置，
        // 注册此组件的一个项目
        if (me.baseAction){
            me.baseAction.addComponent(me);
        }
    },

    /**
     * The initComponent template method is an important initialization step for a Component. It is intended to be
     * implemented by each subclass of Ext.Component to provide any needed constructor logic. The
     * initComponent method of the class being created is called first, with each initComponent method
     * up the hierarchy to Ext.Component being called thereafter. This makes it easy to implement and,
     * if needed, override the constructor logic of the Component at any step in the hierarchy.
     *
     * The initComponent method **must** contain a call to {@link Ext.Base#callParent callParent} in order
     * to ensure that the parent class' initComponent method is also called.
     *
     * All config options passed to the constructor are applied to `this` before initComponent is called,
     * so you can simply access them with `this.someOption`.
     *
     * The following example demonstrates using a dynamic string for the text of a button at the time of
     * instantiation of the class.
     * 
     * 在初始化组件模板方法是一个重要的组件的初始化步骤。它的目的
     * 是要实现Ext.Component提供任何所需的构造逻辑函数每个子类的。
     * 在初始化组件模板被创建的类的方法，
     * 首先会调用层次结构向上Ext.Component被称为此后与每个在InitComponent的方法。
     * 这使得它很容易实现，如果需要的话，重载的构造函数逻辑的组件层次结构中的任何步骤。
     * 在InitComponent方法必须包含一个调用callParent以确保父类的在InitComponent方法是传递给构造函数的called.
     * All的配置选项``被称为前在InitComponent，所以你可以简单地访问他们的this.someOption`。
     * 下面的例子演示了如何使用一个动态的一个按钮的文本字符串的类的实例的时候。
     *
     *     Ext.define('DynamicButtonText', {
     *         extend: 'Ext.button.Button',
     *
     *         initComponent: function() {
     *             this.text = new Date();
     *             this.renderTo = Ext.getBody();
     *             this.callParent();
     *         }
     *     });
     *
     *     Ext.onReady(function() {
     *         Ext.create('DynamicButtonText');
     *     });
     *
     * @template
     * @protected
     */
    initComponent: function() {
        var me = this;

        me.callParent();

        if (me.listeners) {
            me.on(me.listeners);
            me.listeners = null; //change the value to remove any on prototype
        }
        me.enableBubble(me.bubbleEvents);
        me.mons = [];
    },


    // private
    afterRender: function() {
        var me = this;

        me.callParent();

        if (!(me.x && me.y) && (me.pageX || me.pageY)) {
            me.setPagePosition(me.pageX, me.pageY);
        }
    },

    /**
     * Sets the overflow on the content element of the component.
     * 设置溢出的内容元素的组件。
     * @param {Boolean} scroll True to allow the Component to auto scroll.
     * @param {Boolean} 滚动为真让组件自动滚动。
     * @return {Ext.Component} this
     */
    setAutoScroll : function(scroll) {
        var me = this;

        me.autoScroll = !!scroll;

        // Scrolling styles must be applied to Component's main element.
        // 滚动样式必须应用组件的主要元素。
        // Layouts which use an innerCt (Box layout), shrinkwrap the innerCt round overflowing content,
        // 使用一个内（盒布局的布局），收缩包装内的CT轮溢出的内容，
        // so the innerCt must be scrolled by the container, it does not scroll content.
        // 这样的内必须由容器滚动，不滚动的内容。
        if (me.rendered) {
            me.getTargetEl().setStyle(me.getOverflowStyle());
        }
        me.updateLayout();
        return me;
    },

    /**
     * Sets the overflow x/y on the content element of the component. The x/y overflow
     * values can be any valid CSS overflow (e.g., 'auto' or 'scroll'). By default, the
     * value is 'hidden'. Passing null for one of the values will erase the inline style.
     * Passing `undefined` will preserve the current value.
     * 设置溢出x / y的内容元素的组件.x / y的溢出值可以是任何有效的CSS溢出
     *（例如，“自动”或“滚动”）.默认情况下，被'隐藏'。
     * 传递null值将清除内联样式。传递未定义将保持当前值。 
     *
     * @param {String} overflowX The overflow-x value. 
     * overflowX溢出x的值
     * @param {String} overflowY The overflow-y value. 
     * overflowY溢出y的值
     * @return {Ext.Component} this
     */
    setOverflowXY: function(overflowX, overflowY) {
        var me = this,
            argCount = arguments.length;

        if (argCount) {
            me.overflowX = overflowX || '';
            if (argCount > 1) {
                me.overflowY = overflowY || '';
            }
        }

        // Scrolling styles must be applied to Component's main element.
        // 滚动样式必须应用组件的主要元素。
        // Layouts which use an innerCt (Box layout), shrinkwrap the innerCt round overflowing content,
        // 使用一个内（盒布局的布局），收缩包装内的CT轮溢出的内容，
        // so the innerCt must be scrolled by the container, it does not scroll content.
        // 这样的内必须由容器滚动，不滚动的内容。
        if (me.rendered) {
            me.getTargetEl().setStyle(me.getOverflowStyle());
        }
        me.updateLayout();
        return me;
    },

    beforeRender: function () {
        var me = this,
            floating = me.floating,
            cls;

        if (floating) {
            me.addCls(Ext.baseCSSPrefix + 'layer');

            cls = floating.cls;
            if (cls) {
                me.addCls(cls);
            }
        }

        return me.callParent();
    },
    
    afterComponentLayout: function(){
        this.callParent(arguments);
        if (this.floating) {
            this.onAfterFloatLayout();
        }
    },

    // private
    makeFloating : function (dom) {
        this.mixins.floating.constructor.call(this, dom);
    },

    wrapPrimaryEl: function (dom) {
        if (this.floating) {
            this.makeFloating(dom);
        } else {
            this.callParent(arguments);
        }
    },

    initResizable: function(resizable) {
        var me = this;

        resizable = Ext.apply({
            target: me,
            dynamic: false,
            constrainTo: me.constrainTo || (me.floatParent ? me.floatParent.getTargetEl() : null),
            handles: me.resizeHandles
        }, resizable);
        resizable.target = me;
        me.resizer = new Ext.resizer.Resizer(resizable);
    },

    getDragEl: function() {
        return this.el;
    },

    initDraggable: function() {
        var me = this,

            // If we are resizable, and the resizer had to wrap this Component's el (eg an Img)
            // 如果我们可以调整大小，图像缩放此组件的包装EL（例如图）
            // Then we have to create a pseudo-Component out of the resizer to drag that,
            // 然后，我们要创建一个伪组件出图像缩放，拖动，
            // otherwise, we just drag this Component
            // 否则，我们就拖动该组件
            dragTarget = (me.resizer && me.resizer.el !== me.el) ? me.resizerComponent = new Ext.Component({
                el: me.resizer.el,
                rendered: true,
                container: me.container
            }) : me,
            ddConfig = Ext.applyIf({
                el: dragTarget.getDragEl(),
                constrainTo: me.constrain ? (me.constrainTo || (me.floatParent ? me.floatParent.getTargetEl() : me.el.getScopeParent())) : undefined
            }, me.draggable);

        // Add extra configs if Component is specified to be constrained
        // 如果组件添加额外的configs指定要约束
        if (me.constrain || me.constrainDelegate) {
            ddConfig.constrain = me.constrain;
            ddConfig.constrainDelegate = me.constrainDelegate;
        }

        me.dd = new Ext.util.ComponentDragger(dragTarget, ddConfig);
    },

    /**
     * 滚动组件{@link #getTargetEl target element} 的元素由传入三角点，选择动画。
     * Scrolls this Component's {@link #getTargetEl target element} by the passed delta values, optionally animating.
     *
     * 以下是等价的:
     * All of the following are equivalent:
     * 
     *
     *      comp.scrollBy(10, 10, true);
     *      comp.scrollBy([10, 10], true);
     *      comp.scrollBy({ x: 10, y: 10 }, true);
     *
     * @param {Number/Number[]/Object} deltaX要么x三角点,一个数组指定x和y增量或一个对象
     * 以“x”和“y”属性。
     * @param {Number/Number[]/Object} deltaX Either the x delta, an Array specifying x and y deltas or
     * an object with "x" and "y" properties.
     * @param {Number/Boolean/Object} deltaY 无论在y三角点，或一个的动画标志或配置对象。
     * @param {Number/Boolean/Object} deltaY Either the y delta, or an animate flag or config object.
     * @param {Boolean/Object} 动画标志/配置对象的动画，如果增量值分别传递。
     * @param {Boolean/Object} animate Animate flag/config object if the delta values were passed separately.
     */
    scrollBy: function(deltaX, deltaY, animate) {
        var el;

        if ((el = this.getTargetEl()) && el.dom) {
            el.scrollBy.apply(el, arguments);
        }
    },

    /**
     * 此方法允许你显示或隐藏LoadMask这个组件上。
     * This method allows you to show or hide a LoadMask on top of this component.
     *
     * @param {Boolean/Object/String} 加载LoadMask显示默认为true LoadMask构造函数将被传递给一个配置对象，
     * 或显示一个消息字符串。隐藏当前LoadMask为false。
     * @param {Boolean/Object/String} load True to show the default LoadMask, a config object that will be passed to the
     * LoadMask constructor, or a message String to show. False to hide the current LoadMask.
     * @param {Boolean} [targetEl=false]如果为true掩盖的targetEl此组件，而不是的`this.el`。
     * 例如，设置面板上true会导致容器被屏蔽。
     * @param {Boolean} [targetEl=false] True to mask the targetEl of this Component instead of the `this.el`. For example,
     * setting this to true on a Panel will cause only the body to be masked.
     * @return {Ext.LoadMask}  LoadMask 实例
     * @return {Ext.LoadMask} The LoadMask instance that has just been shown.
     */
    setLoading : function(load, targetEl) {
        var me = this,
            config;

        if (me.rendered) {
            Ext.destroy(me.loadMask);
            me.loadMask = null;

            if (load !== false && !me.collapsed) {
                if (Ext.isObject(load)) {
                    config = Ext.apply({}, load);
                } else if (Ext.isString(load)) {
                    config = {msg: load};
                } else {
                    config = {};
                }
                if (targetEl) {
                    Ext.applyIf(config, {
                        useTargetEl: true
                    });
                }
                me.loadMask = new Ext.LoadMask(me, config);
                me.loadMask.show();
            }
        }
        return me.loadMask;
    },

    beforeSetPosition: function () {
        var me = this,
            pos = me.callParent(arguments), // pass all args on for signature decoding
            adj;

        if (pos) {
            adj = me.adjustPosition(pos.x, pos.y);
            pos.x = adj.x;
            pos.y = adj.y;
        }
        return pos || null;
    },

    afterSetPosition: function(ax, ay) {
        this.onPosition(ax, ay);
        this.fireEvent('move', this, ax, ay);
    },

    /**
     * 显示组件在特定XY位置。
     * Displays component at specific xy position.
     * 一个浮动的组件（如菜单）被定位相对如有
     * A floating component (like a menu) is positioned relative to its ownerCt if any.
     * 有用的弹出上下文菜单：
     * Useful for popping up a context menu:
     *
     *     listeners: {
     *         itemcontextmenu: function(view, record, item, index, event, options) {
     *             Ext.create('Ext.menu.Menu', {
     *                 width: 100,
     *                 height: 100,
     *                 margin: '0 0 10 0',
     *                 items: [{
     *                     text: 'regular item 1'
     *                 },{
     *                     text: 'regular item 2'
     *                 },{
     *                     text: 'regular item 3'
     *                 }]
     *             }).showAt(event.getXY());
     *         }
     *     }
     *
     * @param {Number} x The new x position
     * @param {Number} y The new y position
     * @param {Boolean/Object} [animate] True to animate the Component into its new position. You may also pass an
     * animation configuration.
     */
    showAt: function(x, y, animate) {
        var me = this;

        if (!me.rendered && (me.autoRender || me.floating)) {
            me.doAutoRender();
        }
        if (me.floating) {
            me.setPosition(x, y, animate);
        } else {
            me.setPagePosition(x, y, animate);
        }
        me.show();
    },

    /**
     * 设置页面的XY位置的组件。左侧和顶部，而不是要设置，使用{@link #setPosition}方法.
     * Sets the page XY position of the component. To set the left and top instead, use {@link #setPosition}.
     * 该方法触发{@link #move}事件。
     * This method fires the {@link #move} event.
     * @param {Number} 新的X位置
     * @param {Number} x The new x position
     * @param {Number} 新的y位置
     * @param {Number} y The new y position
     * @param {Boolean/Object} [动画]true到新的位置，以动画的组件。您也可以通过
     * 动画配置。
     * @param {Boolean/Object} [animate] True to animate the Component into its new position. You may also pass an
     * animation configuration.
     * @return {Ext.Component} this
     */
    setPagePosition: function(x, y, animate) {
        var me = this,
            p,
            floatParentBox;

        if (Ext.isArray(x)) {
            y = x[1];
            x = x[0];
        }
        me.pageX = x;
        me.pageY = y;

        if (me.floating) {

            // 浮动组件注册容器必须有他们的x和y属性
            // Floating Components which are registered with a Container have to have their x and y properties made relative
            if (me.isContainedFloater()) {
                floatParentBox = me.floatParent.getTargetEl().getViewRegion();
                if (Ext.isNumber(x) && Ext.isNumber(floatParentBox.left)) {
                    x -= floatParentBox.left;
                }
                if (Ext.isNumber(y) && Ext.isNumber(floatParentBox.top)) {
                    y -= floatParentBox.top;
                }
            } else {
                p = me.el.translatePoints(x, y);
                x = p.left;
                y = p.top;
            }

            me.setPosition(x, y, animate);
        } else {
            p = me.el.translatePoints(x, y);
            me.setPosition(p.left, p.top, animate);
        }

        return me;
    },

    // 使用方法来确定，如果一个组件是浮动的，拥有容器其坐标系
    // Utility method to determine if a Component is floating, and has an owning Container whose coordinate system
    // 它必须被定位在使用设置位置时。
    // it must be positioned in when using setPosition.
    isContainedFloater: function() {
        return (this.floating && this.floatParent);
    },

    /**
     * 获取当前框测量相关元件的组件。
     * Gets the current box measurements of the component's underlying element.
     * @param {Boolean} [local=false] 如果真正的元素的左侧和顶部，而不是页面的XY返回。
     * @param {Boolean} [local=false] If true the element's left and top are returned instead of page XY.
     * @return {Object} 对象的格式{X，Y，宽度，高度}
     * @return {Object} box An object in the format {x, y, width, height}
     */
    getBox : function(local){
        var pos = local ? this.getPosition(local) : this.el.getXY(),
            size = this.getSize();

        size.x = pos[0];
        size.y = pos[1];
        return size;
    },

    /**
     * 设置当前框组件的基本元素测量。
     * Sets the current box measurements of the component's underlying element.
     * 对象的格式{X，Y，宽度，高度}
     * @param {Object} box An object in the format {x, y, width, height}
     * @return {Ext.Component} this
     */
    updateBox : function(box){
        this.setSize(box.width, box.height);
        this.setPagePosition(box.x, box.y);
        return this;
    },

    // Include margins
    getOuterSize: function() {
        var el = this.el;
        return {
            width: el.getWidth() + el.getMargin('lr'),
            height: el.getHeight() + el.getMargin('tb')
        };
    },

    // private
    adjustPosition: function(x, y) {
        var me = this,
            floatParentBox;

        // 浮动组件被放置在所属容器的绝对位置。
        // Floating Components being positioned in their ownerCt have to be made absolute.
        if (me.isContainedFloater()) {
            floatParentBox = me.floatParent.getTargetEl().getViewRegion();
            x += floatParentBox.left;
            y += floatParentBox.top;
        }

        return {
            x: x,
            y: y
        };
    },

    /**
     * 获取组件相关元素当前的xy坐标
     * Gets the current XY position of the component's underlying element.
     * @param {Boolean} [local=false] 如果为true就返回元素的左侧和顶部位置,而不会返回页面的xy坐标
     * @param {Boolean} [local=false] If true the element's left and top are returned instead of page XY.
     * @return {Number[]} 元素的xy坐标(例如:[100,200])
     * @return {Number[]} The XY position of the element (e.g., [100, 200])
     */
    getPosition: function(local) {
        var me = this,
            el = me.el,
            xy,
            isContainedFloater = me.isContainedFloater(),
            floatParentBox;

        // 浮动组件被放置在所属容器的相对位置
        // Floating Components which were just rendered with no ownerCt return local position.
        if ((local === true) || !isContainedFloater) {
            return [el.getLeft(true), el.getTop(true)];
        }

        // 如果可能的话，使用我们先前设定的x和y属性..
        // Use our previously set x and y properties if possible.
        if (me.x !== undefined && me.y !== undefined) {
            xy = [me.x, me.y];
        } else {
            xy = me.el.getXY();

            // 浮动在所属容器的组件必须有自己的相对位置
            // Floating Components in an ownerCt have to have their positions made relative
            if (isContainedFloater) {
                floatParentBox = me.floatParent.getTargetEl().getViewRegion();
                xy[0] -= floatParentBox.left;
                xy[1] -= floatParentBox.top;
            }
        }
        return xy;
    },

    getId: function() {
        var me = this,
            xtype;

        if (!me.id) {
            xtype = me.getXType();
            if (xtype) {
                xtype = xtype.replace(Ext.Component.INVALID_ID_CHARS_Re, '-');
            } else {
                xtype = Ext.name.toLowerCase() + '-comp';
            }
            me.id = xtype + '-' + me.getAutoId();
        }
        return me.id;
    },

    /**
     * 显示此组件，使其自动解析{@link #autoRender}或{@link #floating}浮动，如果是'真'。
     * Shows this Component, rendering it first if {@link #autoRender} or {@link #floating} are `true`.
     *
     * 在显示之后，一个{@link #floating}浮动的组分（如一个{@link Ext.window.Window}），
     * 被激活，并且其{@link #zIndexManager z-index stack}的前面。
     * After being shown, a {@link #floating} Component (such as a {@link Ext.window.Window}), is activated it and
     * brought to the front of its {@link #zIndexManager z-index stack}.
     *
     * @param {String/Ext.Element} [animateTarget=null] 仅适用于{@link #floating}组件如
     * {@link Ext.window.Window}或 {@link Ext.tip.ToolTip ToolTip}s，或固定组件已配置浮动：true`。
     * 组件动画从开通时的目标。
     * @param {String/Ext.Element} [animateTarget=null] **only valid for {@link #floating} Components such as {@link
     * Ext.window.Window Window}s or {@link Ext.tip.ToolTip ToolTip}s, or regular Components which have been configured
     * with `floating: true`.** The target from which the Component should animate from while opening.
     * @param {Function} [callback] 显示后的组件调用的回调函数。
     * 只有必要的，如果指定的动画。
     * @param {Function} [callback] A callback function to call after the Component is displayed.
     * Only necessary if animation was specified.
     *  @param {Object} [scope] 作用域 (`this` reference)，其中执行回调。
     * @param {Object} [scope] The scope (`this` reference) in which the callback is executed.
     * Defaults to this Component.
     * @return {Ext.Component} this
     */
    show: function(animateTarget, cb, scope) {
        var me = this,
            rendered = me.rendered;

        if (rendered && me.isVisible()) {
            if (me.toFrontOnShow && me.floating) {
                me.toFront();
            }
        } else if (me.fireEvent('beforeshow', me) !== false) {
            // Render on first show if there is an autoRender config, or if this is a floater (Window, Menu, BoundList etc).
            me.hidden = false;
            if (!rendered && (me.autoRender || me.floating)) {
                me.doAutoRender();
                rendered = me.rendered;
            }
            
            if (rendered) {
                me.beforeShow();
                me.onShow.apply(me, arguments);
                me.afterShow.apply(me, arguments);
            }
        }
        return me;
    },

    /**
     * 显示之前调用组件。
     * Invoked before the Component is shown.
     *
     * @method
     * @template
     * @protected
     */
    beforeShow: Ext.emptyFn,

    /**
     * 允许显示操作的行为。
     * 父类的onShow事件后，组件将是可见的。
     * Allows addition of behavior to the show operation. After
     * calling the superclass's onShow, the Component will be visible.
     *
     * 覆盖在子类中需要更复杂的行为。
     * Override in subclasses where more complex behaviour is needed.
     *
     * 相同的参数被传递
     * Gets passed the same parameters as #show.
     *
     * @param {String/Ext.Element} [animateTarget]
     * @param {Function} [callback]
     * @param {Object} [scope]
     *
     * @template
     * @protected
     */
    onShow: function() {
        var me = this;

        me.el.show();
        me.callParent(arguments);
        if (me.floating && me.constrain) {
            me.doConstrain();
        }
    },

    /**
     * 组件显示后调用
     * Invoked after the Component is shown (after #onShow is called).
     *
     * 相同的参数被传递
     * Gets passed the same parameters as #show.
     *
     * @param {String/Ext.Element} [animateTarget]
     * @param {Function} [callback]
     * @param {Object} [scope]
     *
     * @template
     * @protected
     */
    afterShow: function(animateTarget, cb, scope) {
        var me = this,
            fromBox,
            toBox,
            ghostPanel;

        // 默认配置的目标，如果没有通过
        // Default to configured animate target if none passed
        animateTarget = animateTarget || me.animateTarget;

        // 需要能够克隆组件
        // Need to be able to ghost the Component
        if (!me.ghost) {
            animateTarget = null;
        }
        // 如果我们的动画，从目标到克隆的动画元素*当前框
        // If we're animating, kick of an animation of the ghost from the target to the *Element* current box
        if (animateTarget) {
            animateTarget = animateTarget.el ? animateTarget.el : Ext.get(animateTarget);
            toBox = me.el.getBox();
            fromBox = animateTarget.getBox();
            me.el.addCls(Ext.baseCSSPrefix + 'hide-offsets');
            ghostPanel = me.ghost();
            ghostPanel.el.stopAnimation();

            //分流立即离屏，动画类前抓住它确保无闪烁。
            // Shunting it offscreen immediately, *before* the Animation class grabs it ensure no flicker.
            ghostPanel.el.setX(-10000);

            ghostPanel.el.animate({
                from: fromBox,
                to: toBox,
                listeners: {
                    afteranimate: function() {
                        delete ghostPanel.componentLayout.lastComponentSize;
                        me.unghost();
                        me.el.removeCls(Ext.baseCSSPrefix + 'hide-offsets');
                        me.onShowComplete(cb, scope);
                    }
                }
            });
        }
        else {
            me.onShowComplete(cb, scope);
        }
    },

    /**
     * 方法#afterShow 完成后调用。
     * Invoked after the #afterShow method is complete.
     *
     * 获取通过相同的回调`和`范围，收到的#afterShow参数。
     * Gets passed the same `callback` and `scope` parameters that #afterShow received.
     *
     * @param {Function} [callback]
     * @param {Object} [scope]
     *
     * @template
     * @protected
     */
    onShowComplete: function(cb, scope) {
        var me = this;
        if (me.floating) {
            me.toFront();
            me.onFloatShow();
        }
        Ext.callback(cb, scope || me);
        me.fireEvent('show', me);
        delete me.hiddenByLayout;
    },

    /**
     * Hides this Component, setting it to invisible using the configured {@link #hideMode}.
     * @param {String/Ext.Element/Ext.Component} [animateTarget=null] **only valid for {@link #floating} Components
     * such as {@link Ext.window.Window Window}s or {@link Ext.tip.ToolTip ToolTip}s, or regular Components which have
     * been configured with `floating: true`.**. The target to which the Component should animate while hiding.
     * @param {Function} [callback] A callback function to call after the Component is hidden.
     * @param {Object} [scope] The scope (`this` reference) in which the callback is executed.
     * Defaults to this Component.
     * @return {Ext.Component} this
     */
    hide: function() {
        var me = this;

        // Clear the flag which is set if a floatParent was hidden while this is visible.
        // If a hide operation was subsequently called, that pending show must be hidden.
        me.showOnParentShow = false;

        if (!(me.rendered && !me.isVisible()) && me.fireEvent('beforehide', me) !== false) {
            me.hidden = true;
            if (me.rendered) {
                me.onHide.apply(me, arguments);
            }
        }
        return me;
    },

    /**
     * 动画可能下降到目标元素。
     * Possibly animates down to a target element.
     *
     * 允许行为的隐藏操作。后调用父类onHide的，
     * 组件将被隐藏。
     * Allows addition of behavior to the hide operation. After
     * calling the superclass’s onHide, the Component will be hidden.
     *
     * 相同的参数被传递＃隐藏。
     * Gets passed the same parameters as #hide.
     *
     * @param {String/Ext.Element/Ext.Component} [animateTarget]
     * @param {Function} [callback]
     * @param {Object} [scope]
     *
     * @template
     * @protected
     */
    onHide: function(animateTarget, cb, scope) {
        var me = this,
            ghostPanel,
            toBox;

        // 如果没有通过,默认配置目标.
        // Default to configured animate target if none passed
        animateTarget = animateTarget || me.animateTarget;

        // 必须使用镜像容器
        // Need to be able to ghost the Component
        if (!me.ghost) {
            animateTarget = null;
        }
        // 如果我们开始动画，启动一个动画的镜像到目标
        // If we're animating, kick off an animation of the ghost down to the target
        if (animateTarget) {
            animateTarget = animateTarget.el ? animateTarget.el : Ext.get(animateTarget);
            ghostPanel = me.ghost();
            ghostPanel.el.stopAnimation();
            toBox = animateTarget.getBox();
            toBox.width += 'px';
            toBox.height += 'px';
            ghostPanel.el.animate({
                to: toBox,
                listeners: {
                    afteranimate: function() {
                        delete ghostPanel.componentLayout.lastComponentSize;
                        ghostPanel.el.hide();
                        me.afterHide(cb, scope);
                    }
                }
            });
        }
        me.el.hide();
        if (!animateTarget) {
            me.afterHide(cb, scope);
        }
    },

    /**
     * 容器隐藏后调用的函数
     * Invoked after the Component has been hidden.
     * 
     * 获取通过相同的回调范围`和`作用范围`。
     * Gets passed the same `callback` and `scope` parameters that #onHide received.
     *
     * @param {Function} [callback]
     * @param {Object} [scope]
     *
     * @template
     * @protected
     */
    afterHide: function(cb, scope) {
        var me = this;
        delete me.hiddenByLayout;

        // 我们是在这个级别的后端方法onHide，
        // 但我们建议我们的父类容器可能需要异步...所以callParent不会放弃这里的工作...
        // we are the back-end method of onHide at this level, but our call to our parent
        // may need to be async... so callParent won't quite work here...
        Ext.AbstractComponent.prototype.onHide.call(this);

        Ext.callback(cb, scope || me);
        me.fireEvent('hide', me);
    },

    /**
     * 允许销毁操作的行为。
     * Allows addition of behavior to the destroy operation.
     * 调用父类的OnDestroy后，组件将被销毁。
     * After calling the superclass’s onDestroy, the Component will be destroyed.
     *
     * @template
     * @protected
     */
    onDestroy: function() {
        var me = this;

        // 确保任何辅助成分被破坏
        // Ensure that any ancillary components are destroyed.
        if (me.rendered) {
            Ext.destroy(
                me.proxy,
                me.proxyWrap,
                me.resizer,
                me.resizerComponent
            );

            //比较元器件不同
            // Different from AbstractComponent
            if (me.actionMode == 'container' || me.removeMode == 'container') {
                me.container.remove();
            }
        }
        delete me.focusTask;
        me.callParent();
    },

    deleteMembers: function() {
        var args = arguments,
            len = args.length,
            i = 0;
        for (; i < len; ++i) {
            delete this[args[i]];
        }
    },

    /**
     * 聚焦此组件。
     * Try to focus this component.
     * @param {Boolean} [selectText] 如果适用，true也可以选择这个组件中的文本
     * @param {Boolean} [selectText] If applicable, true to also select the text in this component
     * @param {Boolean/Number} [delay] 延迟焦点的毫秒数（true:10毫秒）。
     * @param {Boolean/Number} [delay] Delay the focus this number of milliseconds (true for 10 milliseconds).
     * @return {Ext.Component} 聚焦的Component。通常的 <code>this</code>的组件。
     * 一些容器可能委托焦点到的({@link Ext.window.Window Window}可以做到这一点通过他们
     * 的{@link Ext.window.Window#defaultFocus defaultFocus} 的配置选项。
     * @return {Ext.Component} The focused Component. Usually <code>this</code> Component. Some Containers may
     * delegate focus to a descendant Component ({@link Ext.window.Window Window}s can do this through their
     * {@link Ext.window.Window#defaultFocus defaultFocus} config option.
     */
    focus: function(selectText, delay) {
        var me = this,
            focusEl,
            focusElDom;

        if (me.rendered && !me.isDestroyed && me.isVisible(true) && (focusEl = me.getFocusEl())) {

            // 得到可能返回FocusEl如果容器组件希望委派重点的后裔。
            // getFocusEl might return a Component if a Container wishes to delegate focus to a descendant.
            // 窗口可以做到这一点通过其defaultFocus的配置可以引用按钮。
            // Window can do this via its defaultFocus configuration which can reference a Button.
            if (focusEl.isComponent) {
                return focusEl.focus(selectText, delay);
            }

            // 如果想延迟，队列调用此功能。
            // If delay is wanted, queue a call to this function.
            if (delay) {
                if (!me.focusTask) {
                    me.focusTask = new Ext.util.DelayedTask(me.focus);
                }
                me.focusTask.delay(Ext.isNumber(delay) ? delay : 10, null, me, [selectText, false]);
                return me;
            }

            //如果它是一个元素与DOM属性
            // If it was an Element with a dom property
            if ((focusElDom = focusEl.dom)) {

                // 不是聚焦元素，添加一个标签索引，编程，使其可成为焦点。    
                // Not a natural focus holding element, add a tab index to make it programatically focusable.
                if (focusEl.needsTabIndex()) {
                    focusElDom.tabIndex = -1;
                }

                // 聚焦元素
                // Focus the element.
                // 焦点拥有了一个DOM焦点侦听器调用该组件的的onfocus的方法来执行特定组件的聚焦处理
                // onfocus的方法来执行特定组件的聚焦处理
                // The focusEl has a DOM focus listener on it which invokes the Component's onFocus method
                // to perform Component-specific focus processing
                focusEl.focus();
                if (selectText === true) {
                    focusElDom.select();
                }
            }

            // 聚焦浮动组件，其堆栈的前面。
            // Focusing a floating Component brings it to the front of its stack.
            // 此执行其zIndexManager。通过防止聚焦为true避免递归。
            // this is performed by its zIndexManager. Pass preventFocus true to avoid recursion.
            if (me.floating) {
                me.toFront(true);
            }
        }
        return me;
    },
    
    /**
     * 此组件上取消任何递延重点
     * Cancel any deferred focus on this component
     * @protected
     */
    cancelFocus: function(){
        var task = this.focusTask;
        if (task) {
            task.cancel();
        }
    },

    // private
    blur: function() {
        var focusEl;
        if (this.rendered && (focusEl = this.getFocusEl())) {
            focusEl.blur();
        }
        return this;
    },

    getEl: function() {
        return this.el;
    },

    // Deprecate 5.0
    getResizeEl: function() {
        return this.el;
    },

    // Deprecate 5.0
    getPositionEl: function() {
        return this.el;
    },

    // Deprecate 5.0
    getActionEl: function() {
        return this.el;
    },

    // Deprecate 5.0
    getVisibilityEl: function() {
        return this.el;
    },

    // Deprecate 5.0
    onResize: Ext.emptyFn,

    // private
    // 实现向上的事件bubbilng策略。默认情况下，组件的气泡事件其所属容器浮动组件目标floatParent的。
    // 组件的气泡事件其所属容器浮动组件目标floatParent的。
    // Implements an upward event bubbilng policy. By default a Component bubbles events up to its ownerCt
    // Floating Components target the floatParent.
    // 某些组件的子类（如菜单）可能实现不同层次的所有权.
    // up（）方法使用则立即找到。 
    // Some Component subclasses (such as Menu) might implement a different ownership hierarchy.
    // The up() method uses this to find the immediate owner.
    getBubbleTarget: function() {
        return this.ownerCt || this.floatParent;
    },

    // private
    getContentTarget: function() {
        return this.el;
    },

    /**
     * 克隆当前组件使用原来的配置传入到该实例的默认值。
     * Clone the current component using the original config values passed into this instance by default.
     * @param {Object} 覆盖一个新的配置包含任何属性来覆盖在克隆版本。
     * @param {Object} overrides A new config containing any properties to override in the cloned version.
     * 可以通过对这个对象的id属性，否则将产生以避免重复。
     * An id property can be passed on this object, otherwise one will be generated to avoid duplicates.
     * @return {Ext.Component} 此组件克隆的克隆副本
     * @return {Ext.Component} clone The cloned copy of this component
     */
    cloneConfig: function(overrides) {
        overrides = overrides || {};
        var id = overrides.id || Ext.id(),
            cfg = Ext.applyIf(overrides, this.initialConfig),
            self;

        cfg.id = id;

        self = Ext.getClass(this);

        // prevent dup id
        return new self(cfg);
    },

    /**
     * 获取此组件的xtype注册{@link Ext.ComponentManager}。
     * 对于列表所有可用xtypes，请看到{@link Ext.Component} 头。用法示例：
     * Gets the xtype for this component as registered with {@link Ext.ComponentManager}. For a list of all available
     * xtypes, see the {@link Ext.Component} header. Example usage:
     *
     *     var t = new Ext.form.field.Text();
     *     alert(t.getXType());  // alerts 'textfield'
     *
     * @return {String} The xtype
     */
    getXType: function() {
        return this.self.xtype;
    },

    /**
     * 找一个容器上面这个组件在任何级别的自定义函数。
     * 如果传递的函数返回true，容器将被退回。
     * Find a container above this component at any level by a custom function. If the passed function returns true, the
     * container will be returned.
     *
     * 请参见{@link Ext.Component#up up}方法
     *
     * @param {Function} fn 自定义函数调用的参数(container, this component).
     * @param {Function} fn The custom function to call with the arguments (container, this component).
     * @return {Ext.container.Container} 第一个容器的自定义函数返回true
     * @return {Ext.container.Container} The first Container for which the custom function returns true
     */
    findParentBy: function(fn) {
        var p;

        // 迭代的所属容器链直到有没有所属容器，我们发现使用选择功能相匹配的起始类。
        // Iterate up the ownerCt chain until there's no ownerCt, or we find an ancestor which matches using the selector function.
        for (p = this.getBubbleTarget(); p && !fn(p, this); p = p.getBubbleTarget()) {
            // do nothing
        }
        return p || null;
    },

    /**
     * 找一个容器上面这个组件在任何级别的xtype或类
     * Find a container above this component at any level by xtype or class
     * 
     * 请参阅{@link Ext.Component#up up}该方法
     * See also the {@link Ext.Component#up up} method.
     *
     * @param {String/Ext.Class} 一个组件的xtype的xtype串直接的组件或类
     * @param {String/Ext.Class} xtype The xtype string for a component, or the class of the component directly
     * @return {Ext.container.Container} 匹配给定的xtype类的第一个容器
     * @return {Ext.container.Container} The first Container which matches the given xtype or class
     */
    findParentByType: function(xtype) {
        return Ext.isFunction(xtype) ?
            this.findParentBy(function(p) {
                return p.constructor === xtype;
            })
        :
            this.up(xtype);
    },

    /**
     * 泡沫起来组件/容器的层次结构，每个组件调用指定的功能。
     * (*this*)函数调用作用域，将规定的范围或当前组件。该函数的参数将提供的
     * args或当前组件。如果在任何时候，该函数返回false泡沫停止。
     * Bubbles up the component/container heirarchy, calling the specified function with each component. The scope
     * (*this*) of function call will be the scope provided or the current component. The arguments to the function will
     * be the args provided or the current component. If the function returns false at any point, the bubble is stopped.
     *
     * @param {Function} 调用的函数
     * @param {Object} [scope] 函数的作用域,默认当前节点
     * @param {Array} [args] 调用函数的参数,默认当前组件
     * @return {Ext.Component} this
     */
    bubble: function(fn, scope, args) {
        var p = this;
        while (p) {
            if (fn.apply(scope || p, args || [p]) === false) {
                break;
            }
            p = p.getBubbleTarget();
        }
        return this;
    },

    getProxy: function() {
        var me = this,
            target;

        if (!me.proxy) {
            target = Ext.getBody();
            if (Ext.scopeResetCSS) {
                me.proxyWrap = target = Ext.getBody().createChild({
                    cls: Ext.resetCls
                });
            }
            me.proxy = me.el.createProxy(Ext.baseCSSPrefix + 'proxy-el', target, true);
        }
        return me.proxy;
    }
});
