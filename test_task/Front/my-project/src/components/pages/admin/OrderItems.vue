<template>
  <div>
    <modal modalHeading=" Update" :cond = "showingEditModal" @modalClose = "showingEditModal = false" >
      <table>
        <tr>
          <td>Shipment Date</td>
          <td>:</td>
          <td><input type="date" placeholder="Shipment Date" v-model="clickedOrderItems.shipmenT_DATE" ></td>
        </tr>
        <tr>
          <td>Status</td>
          <td>:</td>
          <td><select type="text" placeholder="Status" v-model="clickedOrderItems.status">
          <option>Новый</option>
            <option>Выполняется</option>
            <option>Выполнен</option>
          </select>
          </td>
          <td>
            <button class="btnSave" @click= "updateOrderItem()"> Save</button>
          </td>
        </tr>
      </table>
    </modal>
    <h2 class="fleft">Filters</h2>
    <br><br>
    <table>
      <th>Status</th>
      <td>
        <select v-model="filterStatus" @change="filtersByOrders(filterStatus)" >
          <option>All</option>
          <option value="Новый">Новый</option>
          <option value="Выполняется">Выполняется</option>
          <option value="Выполнен">Выполнен</option>

        </select>
      </td>


    </table>
    <button class="addBtn fleft" @click="clearFilters()">Clear Filters</button>

    <table class="nice-table">
      <tr ><th>Order Date</th>
        <th>Shipment Date</th>
        <th>Order Number</th>
        <th>Status</th>
      </tr>
      <tr v-for="order in filtersOrders" >
        <td>{{order.ordeR_DATE}}</td>
        <td>{{order.shipmenT_DATE}}</td>
        <td>{{order.ordeR_NUMBER}}</td>
        <td>{{order.status}}</td>
        <td><button class="edit" @click=" showingEditModal = true , clickedOrderItems = order " >Edit</button></td>
      </tr>
    </table>
  </div>
</template>
<script>
    export default {
        name: 'Users',
        data () {
            return {
                showingEditModal:false,
                orders:[],
                clickedOrderItems:{},
                filterStatus:"",
                sortedOrders:[],
                emptyOrers:[],
                statusNotFound:false

            }
        },
        mounted(){

            this.init();

        },
        methods:{
            init(){
                let userToken= 'Bearer' +" " +localStorage.getItem("token");
                this.$eventBus.$emit("loadingStatus", true);
                let userrid = localStorage.getItem("customerid");
                let url = "http://localhost:56750/api/Order/GetAll"
                this.$axios.get(url, {headers:{ 'Authorization': userToken}} ).then(res =>{
                    this.$eventBus.$emit("loadingStatus", false)
                  //  console.log(res)
                    if(res.data.error){
                        this.$iziToast.error({
                            title: 'Error',
                            message: res.data.message,
                        });
                    }
                    else {
                        this.orders = res.data.data;
                       // console.log(this.orders)
                    }
                })

            },

            updateOrderItem(){
                let userToken= 'Bearer' +" " +localStorage.getItem("token");
                this.$eventBus.$emit("loadingStatus", true);
                this.$axios.put("http://localhost:56750/Api/Order/", this.clickedOrderItems, {headers:{ 'Authorization': userToken}}).then(res =>{
                    this.$eventBus.$emit("loadingStatus", false)
                    if(res.data.success){
                        this.$axios.defaults.headers.common['Authorization'] = 'token' +localStorage.getItem("token");
                       // console.log();
                        this.showingEditModal = false
                        this.init();
                    }
                    else {

                        this.$iziToast.error({
                            title: 'Error',
                            message: "\n" +
                                "",
                        });
                    }
                })

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
            clearFilters(){
                this.filterStatus = null,
                    this.sortedOrders = []
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



