<template>
  <div class="admin">
    <div id="header">
      <h1 class="fleft"> Cart </h1>
      <li>
        <router-link to="/shop/products">
          To Shop
        </router-link>
      </li>

    </div>
    <div id="shop">
      <br>
      <div class="product" v-for="order in filtersOrders" >
        <div>
          <img src="https://www.alternat.ru/upload/iblock/59e/59e0a0ea5ba125ec023d082d634a0f28.jpg">
          <h5>Status:{{order.status}}</h5>
          <br>
          <h5 > Order Date :{{order.orderDate}}</h5>
          <br>
          <h5 > Shipment Date :{{order.shipmentDate}}</h5>
          <br>
          <h5> TotalPrice  :{{order.total}}</h5>
          <br>
          <h5 v-for="product in order.order">Product Name: {{product.name}} Price:{{product.price}} </h5>
          <button  class="addBtn"  @click="clickedOrderId = order.id, DeleteOrder() "> Dell</button>
        </div>
      </div>

    </div>
    <div id="cart">
      <div id="cartContainer">
        <h1>Filters</h1>
        <h3>Status</h3>
        <select  v-model="filterStatus" @change="filtersByOrders(filterStatus)">
          <option>All</option>
          <option value="Новый">Новый</option>
          <option value="Выполняется">Выполняется</option>
          <option value="Выполнен">Выполнен</option>
        </select>
      </div>
    </div>

  </div>
</template>

<script>
    export default {
        name: 'MyOrders',
        data () {
            return {orders:[],
                clickedOrderId:"",
                filterStatus:"",
                sortedOrders:[],
                statusNotFound:false
            }
        },
        mounted(){
            this.initCart()
        },
        methods:{
            initCart(){
                let userToken= 'Bearer' +" " +localStorage.getItem("token");
              //  console.log('Initial Run')
                this.$eventBus.$emit("loadingStatus", true);
                let userid = localStorage.getItem("customerid");
                let url = "http://localhost:56750/api/Order/" + userid
                this.$axios.get(url, {headers:{ 'Authorization': userToken}}).then(res => {
                    this.$eventBus.$emit("loadingStatus", false)
                    if (res.data.error) {
                        this.$iziToast.error({
                            title: 'Error',
                            message: res.data.message,
                        });
                    } else {
                        this.orders = res.data.data;
                       // console.log(this.orders)
                    }
                })
            },
            DeleteOrder() {
                this.initCart()
                let deleteUrl = 'http://localhost:56750/api/Order/' + this.clickedOrderId
                this.$axios.delete(deleteUrl).then(res => { setTimeout(() => 500000);
                } )
                this.initCart()
            },
            filtersByOrders(filterStatus){

                let vm = this;

                this.sortedOrders = []
                //vm.init()
                // console.log(filterStatus)
                this.orders.map(function (item) {
                    if (item.status === filterStatus){

                        vm.sortedOrders.push(item)}
                    else {
                        if(filterStatus === 'All'){

                            vm.statusNotFound = false;
                        }
                        else {
                            vm.statusNotFound = true;



                        }



                    }
                })
            },
        },
        computed:{
            filtersOrders(){
                if (this.sortedOrders.length){
                    return this.sortedOrders
                }
                else {
                    if(this.statusNotFound)
                    {return this.sortedOrders}
                    else {return this.orders }
                }
            },
        }
    }
</script>



